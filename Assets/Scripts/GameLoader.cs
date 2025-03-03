using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;
    public GameObject player;

    public void LoadGame()
    {
        if (difficultySettings != null)
        {
            player.transform.position = new Vector3(7.1f, 0, 3.8f);
            player.transform.rotation = Quaternion.Euler(new Vector3());

            switch (difficultySettings.difficulty)
            {
                case DifficultySettings.DifficultyLevel.Easy:
                    Debug.Log("Difficulty set to Easy");
                    // TODO

                    break;

                case DifficultySettings.DifficultyLevel.Normal:
                    Debug.Log("Difficulty set to Normal");
                    // TODO

                    break;

                case DifficultySettings.DifficultyLevel.Hard:
                    Debug.Log("Difficulty set to Hard");
                    // TODO

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
