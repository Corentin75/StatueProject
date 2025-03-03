using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffLoader : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;
    public void setModeEasy()
    {
        if (difficultySettings == null)
        {
            Debug.LogError("DifficultySettings is NULL! Make sure it is assigned in the Inspector.");
            return;
        }
        difficultySettings.SetDifficulty(DifficultySettings.DifficultyLevel.Easy);
    }
    public void setModeHard()
    {
        if (difficultySettings == null)
        {
            Debug.LogError("DifficultySettings is NULL! Make sure it is assigned in the Inspector.");
            return;
        }
        difficultySettings.SetDifficulty(DifficultySettings.DifficultyLevel.Hard);
    }

    public void setModeNormal()
    {
        if (difficultySettings == null)
        {
            Debug.LogError("DifficultySettings is NULL! Make sure it is assigned in the Inspector.");
            return;
        }
        difficultySettings.SetDifficulty(DifficultySettings.DifficultyLevel.Normal);
    }
}
