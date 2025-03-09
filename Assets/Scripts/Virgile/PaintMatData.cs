using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPaintMat", menuName = "PaintMaterials/PaintMatData")]
public class PaintMatData : ScriptableObject
{
    public string paintMatName;
    public float paintMatPrice;
    public Material paintMatMaterial;
    public Sprite paintMatIcon;
}
