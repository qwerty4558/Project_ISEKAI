using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetItem : MonoBehaviour
{
    [SerializeField] private Result_Item[] test;

    [SerializeField] private TMP_Dropdown dropdown;

    private List<Result_Item> itemIndex;

    private void Awake()
    {
        itemIndex = new List<Result_Item>();
    }

    private void Start()
    {
        SetItemdata(test);
    }

    public void SetItemdata(Result_Item[] items)
    {
        dropdown.ClearOptions();
        itemIndex.Clear();

        List<TMP_Dropdown.OptionData> list = new List<TMP_Dropdown.OptionData>();

        foreach (Result_Item item in items)
        {
            list.Add(new TMP_Dropdown.OptionData(item.result_Item_Name));
            itemIndex.Add(item);
        }

        dropdown.AddOptions(list);
    }

    public void OnDropdownSeleted(Int32 i)
    {
        CraftPuzzleCore.Instance.SetResultItem(itemIndex[i]);
    }
}
