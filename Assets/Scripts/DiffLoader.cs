using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffLoader : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
