using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tools : MonoBehaviour
{
    [SerializeField] private Image image_L;
    [SerializeField] private Image image_C;
    [SerializeField] private Image image_R;

    public void SwitchCurrentTool(PlayerAction[] actions, int index)
    {
        if (actions[index].assignedEquipmentData.UI_Sprite == null) return;

        if (index - 1 >= 0 && actions.Length > 1)
        {
            image_L.gameObject.SetActive(true);
            if (actions[index - 1].assignedEquipmentData.UI_Sprite != null)
                image_L.sprite = actions[index - 1].assignedEquipmentData.UI_Sprite;
        }
        else
        {
            image_L.gameObject.SetActive(false);
        }

        if (index + 1 < actions.Length)
        {
            image_R.gameObject.SetActive(true);
            if (actions[index + 1].assignedEquipmentData.UI_Sprite != null)
                image_R.sprite = actions[index + 1].assignedEquipmentData.UI_Sprite;
        }
        else
        {
            image_R.gameObject.SetActive(false);
        }

        image_C.sprite = actions[index].assignedEquipmentData.UI_Sprite;
        image_C.gameObject.GetComponent<DOTweenAnimation>().DORestart();
    }
}