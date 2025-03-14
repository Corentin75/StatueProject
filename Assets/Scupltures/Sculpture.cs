using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sculpture : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Material cubeMaterial;
    private RenderTexture paintTexture;
    private Texture2D baseTexture;
    private RenderTexture tempTexture;

    public ComputeShader paintComputeShader;
    public float minAlpha = 0.1f;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;

        // Charger la texture de base
        baseTexture = cubeMaterial.mainTexture as Texture2D;
        if (baseTexture == null)
        {
            baseTexture = Texture2D.whiteTexture; // Texture blanche par défaut
        }

        // 🛠 Créer une RenderTexture en format ARGBFloat pour éviter la perte de couleurs
        paintTexture = new RenderTexture(baseTexture.width, baseTexture.height, 0, RenderTextureFormat.ARGBFloat);
        paintTexture.enableRandomWrite = true;
        paintTexture.filterMode = FilterMode.Bilinear;
        paintTexture.Create();

        // 🛠 Créer une texture temporaire pour accumuler les modifications
        tempTexture = new RenderTexture(baseTexture.width, baseTexture.height, 0, RenderTextureFormat.ARGBFloat);
        tempTexture.enableRandomWrite = true;
        tempTexture.filterMode = FilterMode.Bilinear;
        tempTexture.Create();

        // Copier la texture de base dans la texture temporaire et la texture de peinture
        Graphics.Blit(baseTexture, tempTexture);
        Graphics.Blit(tempTexture, paintTexture);

        // Appliquer la texture peinte au matériau
        cubeMaterial.SetTexture("_MainTex", paintTexture);

        // Configurer le Compute Shader
        paintComputeShader.SetTexture(0, "Result", paintTexture);
        paintComputeShader.SetTexture(0, "_MainTex", paintTexture);
        paintComputeShader.SetInts("_TextureSize", paintTexture.width, paintTexture.height);
        paintComputeShader.SetFloat("_MinAlpha", minAlpha);
    }

    public void Paint(Vector2 uv, float brushSize, Texture2D brushTexture, Material paintMaterial, Color paintColor)
    {
        // Inverser Y des coordonnées UV pour correspondre à Unity
        Vector2 hitPosition = new Vector2(uv.x, 1.0f - uv.y);

        // Passer les paramètres au Compute Shader
        paintComputeShader.SetVector("_HitPosition", hitPosition);
        paintComputeShader.SetFloat("_BrushSize", brushSize);
        paintComputeShader.SetVector("_BrushColor", new Vector4(paintColor.r, paintColor.g, paintColor.b, paintColor.a));
        paintComputeShader.SetInts("_TextureSize", paintTexture.width, paintTexture.height);
        paintComputeShader.SetTexture(0, "_BrushTex", brushTexture);

        // 🛠 Assurer que la texture intermédiaire est bien utilisée comme source
        paintComputeShader.SetTexture(0, "_MainTex", tempTexture);
        paintComputeShader.SetTexture(0, "Result", paintTexture);

        // Exécuter le Compute Shader
        int threadGroupX = Mathf.CeilToInt(paintTexture.width / 16f);
        int threadGroupY = Mathf.CeilToInt(paintTexture.height / 16f);
        paintComputeShader.Dispatch(0, threadGroupX, threadGroupY, 1);

        // 🛠 Copier la texture de peinture dans la texture intermédiaire pour éviter l’accumulation d’erreurs
        Graphics.Blit(paintTexture, tempTexture);

        // Appliquer la texture peinte au matériau
        cubeMaterial.SetTexture("_MainTex", paintTexture);
    }
}
