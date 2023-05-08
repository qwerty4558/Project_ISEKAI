using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class LargeDialogueData
{
    public string speecher;
    public Color SpeecherColor;
    [TextArea] public string context;
    [FoldoutGroup("Extras")]
    public Sprite fullImage;
    [FoldoutGroup("Extras")]
    public UnityEvent actions;
}

public class UI_LargeDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speecherText;
    [SerializeField] private TextMeshProUGUI contextText;
    [SerializeField] private Image fullImageUI;
    [SerializeField] private GameObject triangle;

    [SerializeField] private LargeDialogueData[] test_dialogues;

    private float dialogueTextInterval = 0.05f;

    private void Start()
    {
        PlayDialogue(test_dialogues);
    }


    public void PlayDialogue(LargeDialogueData[] dialogues)
    {
        StopAllCoroutines();
        StartCoroutine(Cor_PlayDialogue(dialogues));
    }

    private IEnumerator Cor_PlayDialogue(LargeDialogueData[] dialogues)
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            speecherText.color = dialogues[i].SpeecherColor;
            speecherText.text = dialogues[i].speecher;
            if (dialogues[i].fullImage != null)
            {
                fullImageUI.gameObject.SetActive(true);
                fullImageUI.sprite = dialogues[i].fullImage;
            }
            else
            {
                fullImageUI.gameObject.SetActive(false);
            }
            dialogues[i].actions.Invoke();

            yield return null;

            for(int j = 0; j < dialogues[i].context.Length; j++)
            {
                contextText.text = dialogues[i].context.Substring(0, j);
                bool skipFlag = false;

                for(float t = dialogueTextInterval; t > 0; t -= Time.deltaTime)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        contextText.text = dialogues[i].context;
                        skipFlag = true;
                    }
                    else
                    {
                        yield return null;
                    }
                }

                if (skipFlag) break;

            }

            yield return new WaitForSeconds(0.5f);
            triangle.SetActive(true);
            yield return new WaitUntil(() => Input.GetMouseButton(0));
            triangle.SetActive(false);
        }

    }
}
