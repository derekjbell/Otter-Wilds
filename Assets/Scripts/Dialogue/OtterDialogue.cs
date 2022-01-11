using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtterDialogue : MonoBehaviour
{
    DialogueTrigger dialogue;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("level1") == 0)
        {
            dialogue.TriggerDialogue();
        }
    }
}
