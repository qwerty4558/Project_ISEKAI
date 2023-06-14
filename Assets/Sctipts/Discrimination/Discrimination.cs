using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class Discrimination : MonoBehaviour
{
    private static Discrimination instance;
    public static Discrimination Instance { get { return instance; } }

    [SerializeField] private Image TargetItemUI;
    [SerializeField] private Image TargetPatternUI;
    [SerializeField] private PuzzleIngredientItems itemView;
    [SerializeField] GameObject notice_Warning;
    [SerializeField] SoundModule sound;
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
        itemView.SetItemWindow(InventoryTitle.instance.InventoryMapReturnOnlyIngredient()); // InvenItemMapReturn() -> InventoryMapReturnExceptEquipment() 으로 변경
        notice_Warning.SetActive(false);
        sound = GetComponent<SoundModule>();    
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
        if (ingredient_Item.count > 0)
        {
            TargetItemUI.gameObject.SetActive(true);
            TargetItemUI.GetComponent<DOTweenAnimation>().DORestart();
            TargetPatternUI.gameObject.SetActive(false);
            TargetItemUI.sprite = ingredient_Item.itemImage;
            //TargetPatternUI.sprite = ingredient_Item.itemPatternImage;
            activeIngredient = ingredient_Item;
        }
    }

    public void DoDiscrimination()
    {
        if (activeIngredient == null)
        {
            notice_Warning.SetActive(true);
            sound.Play_No_Isplay("NoHasItem");
        }
        else if (activeIngredient != null && activeIngredient.count > 0)
        {
            sound.Play_No_Isplay("DoDiscrimination");

            TargetPatternUI.gameObject.SetActive(true);
            TargetPatternUI.sprite = activeIngredient.itemPatternImage;
            InventoryTitle.instance.MinusItem(activeIngredient);
            InventoryTitle.instance.AlchemyItemPlus(activeIngredient);

            if (activeIngredient.count == 0) TargetItemUI.gameObject.SetActive(false);

            itemView.SetItemWindow(InventoryTitle.instance.InventoryMapReturnOnlyIngredient());
        }
        else
        {
            notice_Warning.SetActive(true);
            sound.Play_No_Isplay("NoHasItem");
        }
    }
}
