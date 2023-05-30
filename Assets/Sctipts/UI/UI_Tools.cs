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
    [SerializeField] private Image image_EMPTY;

    public void SwitchCurrentTool(PlayerAction[] actions,int index)
    {
        if (actions[index].itemSprite == null) return;

        if (index - 1 >= 0 && actions.Length > 1)
        {
            image_L.gameObject.SetActive(true);
            if (actions[index - 1].CheckStateCondition())
            {
                if (actions[index - 1].itemSprite != null)
                    image_L.sprite = actions[index - 1].itemSprite;
            }
            else
            {

                    image_L.sprite = image_EMPTY.sprite;
            }
        }
        else
        {
            image_L.gameObject.SetActive(false);
        }

        if (index + 1 < actions.Length)
        {
            image_R.gameObject.SetActive(true);
            if (actions[index + 1].CheckStateCondition()) 
            {
                if (actions[index + 1].itemSprite != null)
                    image_R.sprite = actions[index + 1].itemSprite;
            }
            else
            {
            
                    image_R.sprite = image_EMPTY.sprite;
            }
        }
        else
        {
            image_R.gameObject.SetActive(false);
        }

        image_C.sprite = actions[index].itemSprite;
        image_C.gameObject.GetComponent<DOTweenAnimation>().DORestart();
    }
}