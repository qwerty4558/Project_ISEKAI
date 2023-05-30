using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tools : MonoBehaviour
{
    [SerializeField] private Image leftImage;
    [SerializeField] private Image centerImage;
    [SerializeField] private Image rightImage;
    [SerializeField] private Sprite emptyImage;

    [SerializeField] private PlayerAction[] actions;
    [SerializeField] private int index;
    private DOTweenAnimation centerImageAnimation;

    private void Awake()
    {
        centerImageAnimation = centerImage.gameObject.GetComponent<DOTweenAnimation>();
    }

    private void Update()
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        int currentIndex = index;

        // ���� �̹��� ������Ʈ
        if (currentIndex - 1 >= 0 && currentIndex < actions.Length)
        {
            PlayerAction leftAction = actions[currentIndex - 1];
            if (leftAction.CheckStateCondition() && leftAction.itemSprite != null)
            {
                leftImage.gameObject.SetActive(true);
                leftImage.sprite = leftAction.itemSprite;
            }
            else
            {
                leftImage.gameObject.SetActive(true);
                leftImage.sprite = emptyImage;
            }
        }
        else
        {
            leftImage.gameObject.SetActive(false);
        }

        // ������ �̹��� ������Ʈ
        if (currentIndex + 1 < actions.Length)
        {
            PlayerAction rightAction = actions[currentIndex + 1];
            if (rightAction.CheckStateCondition() && rightAction.itemSprite != null)
            {
                rightImage.gameObject.SetActive(true);
                rightImage.sprite = rightAction.itemSprite;
            }
            else
            {
                rightImage.gameObject.SetActive(true);
                rightImage.sprite = emptyImage;
            }
        }
        else
        {
            rightImage.gameObject.SetActive(false);
        }

        // �߾� �̹��� ������Ʈ
        centerImage.sprite = actions[currentIndex].itemSprite;
        centerImageAnimation.DORestart();
    }

    public void SwitchCurrentTool(PlayerAction[] _actions, int _index)
    {
        this.actions = _actions;
        this.index = _index;
        UpdateImage();
    }
}
