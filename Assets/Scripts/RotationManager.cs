using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public DifficultySettings difficultySettings;
    public float speed;
    public int direction = 1;

    void Start()
    {
        speed = difficultySettings.GetRotationSpeed();
    }

    void Update()
    {
        if (difficultySettings.ShouldReverseRotation())
        {
            direction *= -1;
        }
        transform.Rotate(Vector3.up * Time.deltaTime * speed * direction);
    }

    public void ButtonClick()
    {
        speed = difficultySettings.GetRotationSpeed();
    }
}
