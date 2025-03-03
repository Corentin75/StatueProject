using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;

    private Vector3 soclePosition;

    private int currentLevel = 0;
    private int correctStatue;

    public GameObject socle;
    private GameObject currentStructure;
    public GameObject canvaConfirm;
    public GameObject canvaChoice;


    public Button[] buttonsChoice;

    void Start()
    {
        soclePosition = socle.transform.position;
        LoadLevel(currentLevel);
        StartCoroutine(WaitAndIncreaseLevel());
    }

    // Update is called once per frame
    public void LoadLevel(int level)
    {
        if (level < 0 || level >= levels.Length)
        {
            Debug.LogWarning("Niveau invalide : " + level);
            return;
        }

        // Supprime Ancienne Structure
        if (currentStructure != null)
        {
            Destroy(currentStructure);
        }

        // Cr�ation de la prefab du Chefd'oeuvre
        Quaternion instanceRotation = Quaternion.identity;
        currentStructure = Instantiate(levels[level].artWorkPrefab, soclePosition, instanceRotation);
        currentLevel = level;
        LoadImage(currentLevel);
    }

    public void LoadImage(int level)
    {
        for (int i = 0; i < buttonsChoice.Length; i++)
        {
            buttonsChoice[i].image.sprite = levels[level].artWorks[i].artworkPicture;
        }
    }

    public void UpdateLevel()
    {
        int nextLevel = currentLevel + 1;
        if (nextLevel < levels.Length)
        {
            LoadLevel(nextLevel);
            Debug.Log("Passage au niveau : " + nextLevel);
        }
    }

    private IEnumerator WaitAndIncreaseLevel()
    {
        yield return new WaitForSeconds(20f); // Attend 5 secondes

        UpdateLevel();
    }

    public void OnCliCk(Button button)
    {
        Debug.Log("Bouton actuelle : " + button);

        Transform parentTransform = button.transform;

        Quaternion instanceRotation = Quaternion.identity;

        Instantiate(canvaConfirm, soclePosition, instanceRotation, parentTransform);
    }

    private void OpenConfirm()
    {
        canvaConfirm.SetActive(true);
    }
    public void CloseConfirm()
    {
        canvaConfirm.SetActive(false);
    }

}
