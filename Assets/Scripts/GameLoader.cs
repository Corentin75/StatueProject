using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;

    // Start is called before the first frame update
    public void LoadGame()
    {
        if (difficultySettings != null)
        {
            switch (difficultySettings.difficulty)
            {
                case DifficultySettings.DifficultyLevel.Easy:
                    Debug.Log("Difficulty is Easy: Rotation Speed = " + difficultySettings.GetRotationSpeed());
                    break;
                case DifficultySettings.DifficultyLevel.Normal:
                    Debug.Log("Difficulty is Normal: Rotation Speed = " + difficultySettings.GetRotationSpeed());
                    break;
                case DifficultySettings.DifficultyLevel.Hard:
                    Debug.Log("Difficulty is Hard: Rotation Speed = " + difficultySettings.GetRotationSpeed());
                    Debug.Log("Reverse Rotation: " + difficultySettings.ShouldReverseRotation());
                    break;
                default:
                    Debug.Log("Unknown Difficulty");
                    break;
            }
        }
        else
        {
            Debug.LogError("DifficultySettings is not assigned!");
        }
    }
}
