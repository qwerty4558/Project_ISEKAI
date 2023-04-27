using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public static class TMPpoUGUIDoText
{
    public static void DoText(this TextMeshProUGUI a_text, float a_duration)
    {
        a_text.maxVisibleCharacters = 0;
        DOTween.To(x => a_text.maxVisibleCharacters = (int)x, 0f, a_text.text.Length, a_duration);
    }
}