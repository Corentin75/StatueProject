using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffLoader : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;
    public void setModeEasy()
    {
        difficultySettings.SetDifficulty(DifficultySettings.DifficultyLevel.Easy);
    }
    public void setModeHard()
    {
        difficultySettings.SetDifficulty(DifficultySettings.DifficultyLevel.Hard);
    }

    public void setModeNormal()
    {
        difficultySettings.SetDifficulty(DifficultySettings.DifficultyLevel.Normal);
    }
}
