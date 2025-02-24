using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;
    public GameObject socle;
    private Vector3 soclePosition;
    private GameObject currentStructure;
    private int currentLevel = 0;

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

        // Création de la prefab du Chefd'oeuvre
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

}
