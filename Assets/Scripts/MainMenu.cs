using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    SceneTransition scene;

    void Start()
    {
        // reset saved level completion on start
        scene = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
        PlayerPrefs.SetInt("level1", 0);
        PlayerPrefs.SetInt("level2", 0);
        PlayerPrefs.SetInt("level2started", 0);
        PlayerPrefs.SetInt("level3", 0);
        PlayerPrefs.SetInt("level4", 0);
        PlayerPrefs.SetInt("level5", 0);
        PlayerPrefs.SetInt("gamefinished", 0);
        PlayerPrefs.Save();
    }

    public void StartButton()
    {
        scene.LoadNextLevel("Hub1");
    }

    public void QuitButton()
    {
        Debug.Log("Quitting game..");
        Application.Quit();
    }
}
