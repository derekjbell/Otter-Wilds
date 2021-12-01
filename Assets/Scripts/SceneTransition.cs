using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public Animator transition;
    public float transition_time = 1f;

    public void LoadNextLevel(string level_name)
    {
        StartCoroutine(LoadLevel(level_name));
    }

    IEnumerator LoadLevel(string level_name)
    {
        // play the animation
        transition.SetTrigger("Start");
        // wait
        yield return new WaitForSeconds(transition_time);
        // load the new scene
        SceneManager.LoadScene(level_name);
    }
}
