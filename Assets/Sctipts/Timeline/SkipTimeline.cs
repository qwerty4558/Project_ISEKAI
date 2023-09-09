using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SkipTimeline : MonoBehaviour
{
    [SerializeField] private GameObject buttonObject;

    public void ToggleSkip(bool value)
    {
        if(value)
        {
            buttonObject.SetActive(true);
        }
        else
        {
            buttonObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(buttonObject.activeInHierarchy)
            {
                OnSkipButtonDown();
            }
        }
    }

    public void OnSkipButtonDown()
    {
        Debug.Log("SKIP");

        var plDirs = FindObjectsOfType<PlayerPlayableControl>();
        foreach(var plDir in plDirs)
        {
            if(plDir.gameObject.activeInHierarchy)
            {
                plDir.SkipCutscene();
            }
        }

        var dialogueConts = FindObjectsOfType<DialogueContainer>();
        foreach (var dialogueCont in dialogueConts)
        {
            if (dialogueCont.gameObject.activeInHierarchy)
                dialogueCont.AbortDialogue();
        }
    }
}
