using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public string level_to_show_dialogue;

    //bool level_2_started = false;

    public void Awake()
    {
        if (PlayerPrefs.GetInt("level1") == 0 && level_to_show_dialogue == "EntranceHub1")
        {
            TriggerDialogue();
        }
        else if (PlayerPrefs.GetInt("level1") == 0 && level_to_show_dialogue == "level1")
        {
            TriggerDialogue();
        }
        else if (PlayerPrefs.GetInt("level2") == 0 && level_to_show_dialogue == "level2" && PlayerPrefs.GetInt("level2started") == 0)
        {
            PlayerPrefs.SetInt("level2started", 1);
            TriggerDialogue();
        }
        else if (PlayerPrefs.GetInt("level1") == 1 && PlayerPrefs.GetInt("level2") == 1 && PlayerPrefs.GetInt("level3") == 1 && level_to_show_dialogue == "Hub1")
        {
            TriggerDialogue();
        }
        else if (level_to_show_dialogue == "level3" && PlayerPrefs.GetInt("level3") == 0)
        {
            TriggerDialogue();
        }
        else if (level_to_show_dialogue == "Finale")
        {
            TriggerDialogue();
        }
        else if (level_to_show_dialogue == "Ending")
        {
            PlayerPrefs.SetInt("gamefinished", 1);
            TriggerDialogue();
        }

    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
