using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewArtworkData", menuName = "Artworks/ArtworkData")]
public class ArtworkData : ScriptableObject
{
    public string artworkName;
    public GameObject artworkPrefab;
}
