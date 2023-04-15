using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tools : MonoBehaviour
{
    [SerializeField] private Image image_L;
    [SerializeField] private Image image_C;
    [SerializeField] private Image image_R;

    [SerializeField] private Sprite[] toolImages;   //NOT SAFE, TEMPOARY DATA

    private int toolIndex = 0;

    public void SwitchCurrentTool(ActionState state)
    {
        if (toolImages == null) return;

        int intState = (int)state;

        if (intState - 1 >= 0 && toolImages.Length > 1)
        {
            image_L.enabled = true;
            image_L.sprite = toolImages[intState - 1];
        }
        else
        {
            image_L.enabled = false;
        }

        if (intState + 1 < toolImages.Length)
        {
            image_R.enabled = true;
            image_R.sprite = toolImages[intState + 1];
        }
        else
        {
            image_R.enabled = false;
        }


        image_C.sprite = toolImages[intState];
    }
}

