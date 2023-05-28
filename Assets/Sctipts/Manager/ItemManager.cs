using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public List<Ingredient_Item> all_Items; 

    public void ItemDataInitailize()
    {
        int _listCount = all_Items.Count;
        for(int i = 0; i < _listCount; ++i)
        {
            all_Items[i].count = 0;
        }
    }


}

