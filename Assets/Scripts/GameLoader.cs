using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] public DifficultySettings difficultySettings;
    public GameObject player;

    public TMP_Text timerText;
    private float timer;
    private bool isTimerRunning = false;




    private void Update()
    {        
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            timerText.text = $"Time: {timer:F1} s";
        }
    }

    public void LoadGame()
    {
        if (difficultySettings != null)
        {
            player.transform.position = new Vector3(1.46f, 0.212f, 0.13f); // tp dans l'atelier


            timer = 0f;
            isTimerRunning = true;
            timerText.gameObject.SetActive(true);
            timerText.text = $"Time: {timer:F1} s";
                    Debug.Log("Timer started");

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

    public void EndGame()
    {
        Debug.Log("Game over");
        player.transform.position = new Vector3(-6.6f, 0, -1.85f); // tp dans le local

        isTimerRunning = false;
        timerText.gameObject.SetActive(false);
        Debug.Log("Timer stopped");


        int moneyEarned = 50;
        if (timer < 100)
            moneyEarned += Mathf.CeilToInt(100 - timer);

        switch (difficultySettings.difficulty)
        {
            case DifficultySettings.DifficultyLevel.Normal:
                moneyEarned = Mathf.CeilToInt(moneyEarned * 0.8f);
                break;
            case DifficultySettings.DifficultyLevel.Hard:
                moneyEarned = Mathf.CeilToInt(moneyEarned * 0.5f);
                break;
            default:
                break;
        }

        MoneyInventory.Instance.money += moneyEarned;
        MoneyInventory.Instance.UpdateMoneyUI();

        Debug.Log("Money earned: " + moneyEarned);
        Debug.Log("Time taken: " + timer.ToString("F1")); // arrondi au dixième
    }

    public void QuitGame()
    {
        //Application.Quit();                // build
        EditorApplication.isPlaying = false; // editor
    }
}
