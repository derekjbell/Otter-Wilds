using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{

    SceneTransition scene;
    public string LevelToLoad;

    public string level_completed;

    void Start()
    {
        scene = GameObject.Find("SceneTransition").GetComponent<SceneTransition>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (level_completed == "level1")
        {
            PlayerPrefs.SetInt("level1", 1);
            PlayerPrefs.Save();
        }
        else if (level_completed == "level2")
        {
            PlayerPrefs.SetInt("level2", 1);
            PlayerPrefs.Save();
        }
        else if (level_completed == "level3")
        {
            PlayerPrefs.SetInt("level3", 1);
            PlayerPrefs.Save();
        }
        else if (level_completed == "level4")
        {
            PlayerPrefs.SetInt("level4", 1);
            PlayerPrefs.Save();
        }
        else if (level_completed == "level5")
        {
            PlayerPrefs.SetInt("level5", 1);
            PlayerPrefs.Save();
        }
        scene.LoadNextLevel(LevelToLoad);
    }

    void LevelCompletion()
    {

    }

}
