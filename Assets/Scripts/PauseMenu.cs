using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseButton();
            }
        }
    }

    void PauseButton()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeButton()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void RestartLevelButton()
    {
        Debug.Log("Restarting level..");
        ResumeButton();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void MenuButton()
    {
        ResumeButton();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Going to main menu..");
    }

    public void QuitButton()
    {
        Debug.Log("Quiting game..");
        Application.Quit();
    }
}
