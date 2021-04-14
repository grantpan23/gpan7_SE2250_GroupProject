using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    private GameObject[] enemies;
    // Start is called before the first frame update
    void Awake()
    {
        LoadCharacter();
    }

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("enemy");
    }

    
    void LateUpdate()
    {
        if (AllEnemiesDead() && UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("KingArthurQuest")) 
        {
            StartCoroutine(LoadNextLevel());
        }

        GameObject[] chalice = GameObject.FindGameObjectsWithTag("chalice");
        if(chalice.Length == 0 && UnityEngine.SceneManagement.SceneManager.GetActiveScene() == UnityEngine.SceneManagement.SceneManager.GetSceneByName("Level2"))
        {
            StartCoroutine(LoadVictoryScreen());
        }
    }

    private void LoadCharacter()
    {
        int characterIndex = PlayerPrefs.GetInt("CharacterIndex");

        Instantiate(characterPrefabs[characterIndex], new Vector3(-7,0,0), Quaternion.identity);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    IEnumerator LoadVictoryScreen()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    private bool AllEnemiesDead()
    {
        bool allDead = true;    
        foreach(GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                allDead = false;
            }
        }
        return allDead;
    }
}


