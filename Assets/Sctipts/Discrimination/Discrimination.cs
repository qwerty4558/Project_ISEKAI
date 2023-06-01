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
    [SerializeField] private PuzzleIngredientItems itemView;

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

        if(InventoryTitle.instance != null)
        itemView.SetItemWindow(InventoryTitle.instance.InventoryMapReturnExceptEquipment()); // InvenItemMapReturn() -> InventoryMapReturnExceptEquipment() 으로 변경
    }

    private void OnDisable()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            player.ControlEnabled = true;
        CursorManage.instance.HideMouse();
    }

    private void Update()
    {
        if (gameObject.activeSelf) CursorManage.instance.ShowMouse();
    }

    public void TryDiscrimination(Ingredient_Item ingredient_Item)
    {
        TargetItemUI.gameObject.SetActive(true);
        TargetPatternUI.gameObject.SetActive(false);
        TargetItemUI.sprite = ingredient_Item.itemImage;
        //TargetPatternUI.sprite = ingredient_Item.itemPatternImage;
        activeIngredient = ingredient_Item;
    }

    public void DoDiscrimination(Ingredient_Item ingredient_Item)
    {
        if (activeIngredient == null) return;
        TargetPatternUI.gameObject.SetActive(true);
        TargetPatternUI.sprite = ingredient_Item.itemPatternImage;
        InventoryTitle.instance.MinusItem(activeIngredient);
        InventoryTitle.instance.AlchemyItemPlus(activeIngredient);
        itemView.SetItemWindow(InventoryTitle.instance.InvenItemMapReturn());

    }
}
