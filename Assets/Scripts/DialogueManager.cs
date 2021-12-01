using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogue_text;
    public Animator animator;
    private Queue<string> sentences = new Queue<string>();

    // private void Awake()
    // {
    //     sentences = new Queue<string>();
    // }

    public void StartDialogue(Dialogue dialogue)
    {

        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogue_text.text = sentence;
    }

    void EndDialogue()
    {
        if (PlayerPrefs.GetInt("gamefinished") == 1)
        {
            Application.Quit();
        }
        animator.SetBool("isOpen", false);
    }
}
