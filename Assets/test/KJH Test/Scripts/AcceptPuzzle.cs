using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptPuzzle : MonoBehaviour
{
    [SerializeField] int index;

    private ActiveCore activeCore;
    public Result_Item[] table_Item;
    List<Result_Item> result_Items = new List<Result_Item>();

    [SerializeField] bool isPuzzleActive = false;

    // Start is called before the first frame update
    void OnEnable()
    {
        activeCore = FindObjectOfType<ActiveCore>();
        for (int i = 0; i < table_Item.Length; i++)
        {
            result_Items.Add(table_Item[i]);
        }
    }

    public void OpenCore()
    {
        MultisceneDatapass.Instance.craftableItems = result_Items;
        activeCore.OpenCore(this.gameObject, index);
        /*TargetItem targetItem = FindObjectOfType<TargetItem>();
        targetItem.SetItemdata(table_Item);*/
        
        
    }
}
