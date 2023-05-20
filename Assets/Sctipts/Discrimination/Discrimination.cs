using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Discrimination : MonoBehaviour
{
    private static Discrimination instance;
    public static Discrimination Instance { get { return instance; } }

    [SerializeField] private Image TargetItemUI;
    [SerializeField] private Image TargetPatternUI;

    private Ingredient_Item activeIngredient;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        activeIngredient = null;
        TargetItemUI.gameObject.SetActive(false);
        TargetPatternUI.gameObject.SetActive(false);

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.ControlEnabled = false;
    }

    private void OnDisable()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.ControlEnabled = true;
    }



    public void TryDiscrimination(Ingredient_Item ingredient_Item)
    {
        TargetItemUI.sprite = ingredient_Item.itemImage;
        TargetPatternUI.sprite = ingredient_Item.itemPatternImage;
        activeIngredient = ingredient_Item;

        TargetItemUI.gameObject.SetActive(true);
        TargetPatternUI.gameObject.SetActive(true);

        if (activeIngredient.count != 0)
        {
            TargetItemUI.color = new Color(1, 1, 1, 1);
        }
        else
        {
            TargetItemUI.color = new Color(1, 1, 1, 0.5f);
        }
    }

    public void DoDiscrimination()
    {
        if (activeIngredient == null) return;

        if (activeIngredient.count != 0)
        {
            InventoryTitle.instance.MinusItem(activeIngredient);
            InventoryTitle.instance.AlchemyItemPlus(activeIngredient);

            if (activeIngredient.count != 0)
            {
                TargetItemUI.color = new Color(1, 1, 1, 1);
            }
            else
            {
                TargetItemUI.color = new Color(1, 1, 1, 0.5f);
            }
        }


    }
}
