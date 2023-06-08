using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Discrimination_Tuto_Num : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void MinusOneToText()
    {
        int count;
        if(int.TryParse(text.text,out count))
        {
            count--;
            text.text = count.ToString();
        }
    }
}
