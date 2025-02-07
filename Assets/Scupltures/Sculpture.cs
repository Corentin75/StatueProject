using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sculpture : MonoBehaviour
{
    public Color paintColor = Color.red;
    public float brushSize = 0.1f;
    private Renderer cubeRenderer;
    private Material cubeMaterial;
    private RenderTexture paintTexture;
    private Texture2D baseTexture; // Image de base

    public ComputeShader paintComputeShader;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeMaterial = cubeRenderer.material;

        // Charger la texture de base (si ton matériau a une texture)
        baseTexture = cubeMaterial.GetTexture("_MainTex") as Texture2D;
        if (baseTexture == null)
        {
            baseTexture = Texture2D.whiteTexture; // Texture blanche si aucune
        }

        // Créer la RenderTexture pour la peinture
        paintTexture = new RenderTexture(baseTexture.width, baseTexture.height, 0);
        paintTexture.enableRandomWrite = true;
        paintTexture.filterMode = FilterMode.Bilinear;
        paintTexture.Create();

        // Copier la texture de base dans paintTexture
        Graphics.Blit(baseTexture, paintTexture);

        // Assigner la texture peinte au matériau
        cubeMaterial.SetTexture("_MainTex", paintTexture);

        // Passer les textures au Compute Shader
        paintComputeShader.SetTexture(0, "Result", paintTexture);
        paintComputeShader.SetTexture(0, "_MainTex", paintTexture);
        paintComputeShader.SetInts("_TextureSize", paintTexture.width, paintTexture.height);
    }

    public void Paint(Vector2 uv, float brushSize)
    {
        Debug.Log("UV Hit Position: " + uv); // Afficher la position UV dans la console

        // Inverser Y des coordonnées UV pour Unity (Y=0 en bas)
        Vector2 hitPosition = new Vector2(uv.x, 1.0f - uv.y); // Inverser Y pour Unity
        Debug.Log("Inverted UV Hit Position: " + hitPosition); // Afficher la position UV inversée

        // Passer la position du pinceau et la taille du pinceau au Compute Shader
        paintComputeShader.SetVector("_HitPosition", hitPosition);  // Position du pinceau (UV normalisé)
        paintComputeShader.SetFloat("_BrushSize", brushSize / Mathf.Max(paintTexture.width, paintTexture.height));  // Taille du pinceau en proportion de la texture
        paintComputeShader.SetVector("_BrushColor", new Vector4(paintColor.r, paintColor.g, paintColor.b, paintColor.a));
        paintComputeShader.SetInt("_TextureSize", paintTexture.width);

        // Passer la texture au Compute Shader
        paintComputeShader.SetTexture(0, "_MainTex", paintTexture);

        // Lancer le Compute Shader
        int threadGroupX = Mathf.CeilToInt(paintTexture.width / 16f);
        int threadGroupY = Mathf.CeilToInt(paintTexture.height / 16f);
        paintComputeShader.Dispatch(0, threadGroupX, threadGroupY, 1);

        // Appliquer la texture modifiée au matériau
        cubeMaterial.SetTexture("_MainTex", paintTexture);
    }
}
