using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;

    private Vector3 soclePosition;
    private Vector3 statuePosition;

    private int currentLevel = 0;
    private int levelClick = 0;

    public GameObject socle;
    public GameObject statueEmplacement;
    public GameObject canvaConfirm;
    public GameObject canvaHome;
    public GameObject canvaRight;
    public GameObject canvaLeft;

    private GameObject currentStructure;
    private GameObject currentStatue;

    private int badAnswersNb;

    void Start()
    {
        soclePosition = socle.transform.position + Vector3.up * 0.5f;
        statuePosition = statueEmplacement.transform.position;

        LoadLevel(currentLevel);
        Debug.Log("Niveau actuel : " + currentLevel);

        
    }

    public void LoadLevel(int level)
    {
        if (level < 0 || level >= levels.Length) return;

        if (currentStructure) Destroy(currentStructure);

        currentStructure = Instantiate(levels[level].HideArtWork.artworkPrefab, soclePosition, Quaternion.Euler(90, 0, 0), socle.transform);
        currentLevel = level;

        LoadStatue();
    }

    public void LoadStatue()
    {
        if (currentStatue) Destroy(currentStatue);

        currentStatue = Instantiate(levels[currentLevel].artWorks[levelClick].artworkPrefab, statuePosition, Quaternion.Euler(-90, 0, 0), statueEmplacement.transform);
        currentStatue.transform.localScale = Vector3.one * 60f;
    }

    public void NextLevel()
    {
        if (currentLevel < levels.Length - 1)
        {
            currentLevel++;
            LoadLevel(currentLevel);
            Debug.Log("Passage au niveau : " + currentLevel);
        }
        else
        {
            Debug.Log("Tous les niveaux ont été terminés !");
        }
    }


    public void OnCliCk() {
        canvaConfirm.SetActive(true);
        canvaLeft.SetActive(false);
        canvaRight.SetActive(false);
    }
    public void CloseConfirm()
    {
        canvaConfirm.SetActive(false);
        canvaLeft.SetActive(true);
        canvaRight.SetActive(true);
    }

    public void OpenConfirm()
    {
        bool isCorrect = levels[currentLevel].HideArtWork == levels[currentLevel].artWorks[levelClick];
        
        if(isCorrect)
        {
            Debug.Log("Good Answer");
            canvaHome.SetActive(true);
        }
        else
        {
            Debug.Log("Bad Answer");
            badAnswersNb++;
            Debug.Log("Bad answers number: " + badAnswersNb);
        }
        canvaConfirm.SetActive(false);
    }

    public void RightClick()
    {
        levelClick = (levelClick + 1) % levels[currentLevel].artWorks.Length;
        LoadStatue();
    }

    public void LeftClick()
    {
        levelClick = (levelClick - 1 + levels[currentLevel].artWorks.Length) % levels[currentLevel].artWorks.Length;
        LoadStatue();
    }

    public void OnClickEndGame()
    {
        NextLevel();
        canvaHome.SetActive(false);
        badAnswersNb = 0;
    }

}
