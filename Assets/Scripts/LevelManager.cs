using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;
    public GameObject socle;
    private Vector3 soclePosition;
    private GameObject currentStructure;
    private int currentLevel = 0;

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
    }

    public void UpdateLevel()
    {
        int nextLevel = currentLevel + 1;
        if (nextLevel < levels.Length)
        {
            LoadLevel(nextLevel);
        }
    }

    private IEnumerator WaitAndIncreaseLevel()
    {
        yield return new WaitForSeconds(5f); // Attend 10 secondes

        int nextLevel = currentLevel + 1;
        if (nextLevel < levels.Length)
        {
            LoadLevel(nextLevel);
            Debug.Log("Passage au niveau : " + nextLevel);
        }
        else
        {
            Debug.Log("Tous les niveaux ont été complétés !");
        }
    }

}
