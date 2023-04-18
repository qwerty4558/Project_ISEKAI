using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class testTMP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI testtext;
    TextMeshPro testMeshPro;

    private void Start()
    {
        TMPpoUGUIDoText.DoText(testtext, 5f);

    }

    private void Update()
    {

        


    }
    
}


public static class TMPpoUGUIDoText
{
    public static void DoText(this TextMeshProUGUI a_text, float a_duration)
    {
        a_text.maxVisibleCharacters = 0;
        DOTween.To(x => a_text.maxVisibleCharacters = (int)x, 0f, a_text.text.Length, a_duration);
    }
}