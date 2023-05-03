using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class ItemManager : GameManager
{
    public TextAsset itemDatabase; // 재료 아이템

    public List<Ingredient_Item> all_Items; 

    private void Start()
    {
        InitItemDatabase();        
        Save();
    }

    private void InitItemDatabase()
    {
        string[] line = itemDatabase.text.Substring(0, itemDatabase.text.Length - 1).Split('\n');
        int length = line.Length;
        for (int i = 0; i < length; ++i)
        {
            string[] row = line[i].Split('\t');
            row[3] = row[3].Replace("\r", "");
            //all_Items.Add(new Ingredient_Item(row[0], row[1], row[2], row[3])); 아이템 데이터 바뀜
        }
    }
    void Save()
    {
        string jdata = JsonConvert.SerializeObject(all_Items);
        File.WriteAllText(Application.dataPath + "/MainProjectFolder/Item/ItemDatabase/MyItemText.txt", jdata);        
    }
}

