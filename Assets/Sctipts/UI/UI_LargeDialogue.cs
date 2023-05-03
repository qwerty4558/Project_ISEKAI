using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class LargeDialogueData
{
    public string speecher;
    public Color SpeecherColor;
    public string context;

}

public class UI_LargeDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speecherText;
    [SerializeField] private TextMeshProUGUI ContextText;
}
