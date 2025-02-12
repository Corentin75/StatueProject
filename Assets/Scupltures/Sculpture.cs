using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sculpture : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Material cubeMaterial;
    private RenderTexture paintTexture;
    private Texture2D baseTexture; // Image de base
    private RenderTexture tempTexture; // Texture intermédiaire

    public ComputeShader paintComputeShader;
    public float minAlpha = 0.1f; // Alpha minimum

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;
        Debug.Log("Cube Material: " + cubeMaterial);
        // Vérifier si le mesh utilise une texture existante
        baseTexture = cubeMaterial.mainTexture as Texture2D;
        if (baseTexture == null)
        {
            baseTexture = Texture2D.whiteTexture; // Si aucune texture n'existe, utiliser une texture blanche
        }
        Debug.Log("Base Texture: " + baseTexture);
        // Créer une nouvelle RenderTexture
        paintTexture = new RenderTexture(baseTexture.width, baseTexture.height, 0);
        paintTexture.enableRandomWrite = true;
        paintTexture.filterMode = FilterMode.Bilinear;
        paintTexture.Create();

        // Créer une texture intermédiaire
        tempTexture = new RenderTexture(baseTexture.width, baseTexture.height, 0);
        tempTexture.enableRandomWrite = true;
        tempTexture.filterMode = FilterMode.Bilinear;
        tempTexture.Create();

        // Copier la texture existante dans la texture intermédiaire
        Graphics.Blit(baseTexture, tempTexture);

        // Copier la texture intermédiaire dans la texture de peinture
        Graphics.Blit(tempTexture, paintTexture);

        Debug.Log("Base Texture: " + paintTexture);

        // Appliquer la texture peinte au matériau
        cubeMaterial.SetTexture("_MainTex", paintTexture);

        // Configurer le matériau pour utiliser le shader personnalisé
        cubeMaterial.shader = Shader.Find("Custom/PaintShader");

        // Passer la texture au Compute Shader
        paintComputeShader.SetTexture(0, "Result", paintTexture);
        paintComputeShader.SetTexture(0, "_MainTex", paintTexture);
        paintComputeShader.SetInts("_TextureSize", paintTexture.width, paintTexture.height);

        // Passer la valeur de l'alpha minimum au Compute Shader
        paintComputeShader.SetFloat("_MinAlpha", minAlpha);
    }

    public void Paint(Vector2 uv, float brushSize, Texture2D brushTexture, Material paintMaterial, Color paintColor)
    {
        // Inverser Y des coordonnées UV pour correspondre à la disposition de la texture dans Unity
        Vector2 hitPosition = new Vector2(uv.x, 1.0f - uv.y); // Correction des coordonnées UV

        paintComputeShader.SetVector("_HitPosition", hitPosition);  // Passer les coordonnées UV inversées
        paintComputeShader.SetFloat("_BrushSize", brushSize);  // Passer la taille du pinceau
        paintComputeShader.SetVector("_BrushColor", new Vector4(paintColor.r, paintColor.g, paintColor.b, paintColor.a));
        paintComputeShader.SetInts("_TextureSize", paintTexture.width, paintTexture.height);

        // Passer la texture de pinceau au Compute Shader
        paintComputeShader.SetTexture(0, "_BrushTex", brushTexture);

        // Passer la texture intermédiaire au Compute Shader
        paintComputeShader.SetTexture(0, "_MainTex", tempTexture);
        paintComputeShader.SetTexture(0, "Result", paintTexture); // Utiliser la texture de peinture pour écrire

        // Passer la valeur de l'alpha minimum au Compute Shader
        paintComputeShader.SetFloat("_MinAlpha", minAlpha);

        // Lancer le Compute Shader
        int threadGroupX = Mathf.CeilToInt(paintTexture.width / 16f);
        int threadGroupY = Mathf.CeilToInt(paintTexture.height / 16f);
        paintComputeShader.Dispatch(0, threadGroupX, threadGroupY, 1);

        // Copier la texture de peinture dans la texture intermédiaire pour accumuler les taches
        Graphics.Blit(paintTexture, tempTexture);

        // Appliquer la texture modifiée au matériau
        cubeMaterial.SetTexture("_MainTex", paintTexture);
    }
}
