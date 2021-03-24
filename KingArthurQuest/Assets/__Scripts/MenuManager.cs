using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] characters;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGameAsSwordsman()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("CharacterIndex", 0);
    }

    public void StartGameAsArcher()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("CharacterIndex", 1);
    }
}
