using UnityEngine;

[CreateAssetMenu(fileName = "DifficultySettings", menuName = "GameSettings/Difficulty")]
public class DifficultySettings : ScriptableObject
{
    public enum DifficultyLevel { Easy, Normal, Hard }
    public DifficultyLevel difficulty = DifficultyLevel.Normal;

    public float GetRotationSpeed()
    {
        switch (difficulty)
        {
            case DifficultyLevel.Easy:
                return 10f;
            case DifficultyLevel.Normal:
                return 30f;
            case DifficultyLevel.Hard:
                return 80f;
            default:
                return 20f;
        }
    }

    public bool ShouldReverseRotation()
    {
        if (difficulty == DifficultyLevel.Hard)
        {
            return Random.Range(0, 400) < 1; 
        }
        return false;
    }
    public void SetDifficulty(DifficultyLevel newDifficulty)
    {
        difficulty = newDifficulty;
    }
}
