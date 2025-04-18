using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "LevelDatas/LevelData")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int levelId;
    public ArtworkData HideArtWork;
    public ArtworkData[] artWorks;
}
