using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DatabaseToScriptableObject : EditorWindow
{
    [MenuItem("Window/Database to ScriptableObject")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DatabaseToScriptableObject));
    }

    private TextAsset itemDatabase;
    private TextAsset resultDatabase;

    private void OnGUI()
    {
        GUILayout.Label("Make Item", EditorStyles.boldLabel);        
        
        itemDatabase = EditorGUILayout.ObjectField("IngridentItem", itemDatabase, typeof(TextAsset), false) as TextAsset;
        resultDatabase = EditorGUILayout.ObjectField("ResultItem", resultDatabase, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Ingredient Convert"))
        {
            ConvertIngredientItem();
        }
        if (GUILayout.Button("Result Convert"))
        {
            ConvertResultItem();
        }
    }

    private void ConvertResultItem()
    {
        if(resultDatabase == null) 
        {
            Debug.LogError("Please set the database file to convert");
            return;
        }
        string[] line = resultDatabase.text.Substring(0,resultDatabase.text.Length-1).Split('\n');

        List<string[]> data = new List<string[]>();
        int length = line.Length;
        for(int i = 0; i < length; i++)
        {
            string[] row = line[i].Split("\t");
            row[8] = row[8].Replace("\r", "");
            data.Add(row);
        }

        for(int i = 0; i < data.Count; i++)
        {
            Result_Item item = ScriptableObject.CreateInstance<Result_Item>();
            item = new Result_Item(data[i][0], data[i][1], data[i][2], data[i][3], data[i][4], data[i][5], data[i][6], data[i][7], data[i][8]);
            AssetDatabase.CreateAsset(item, "Assets/MainProjectFolder/Item/ScriptableOBj/Result/Result_" + item.result_Item_Name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void ConvertIngredientItem()
    {
        if (itemDatabase == null)
        {
            Debug.LogError("Please set the database file to convert");
            return;
        }

        string[] line = itemDatabase.text.Substring(0, itemDatabase.text.Length - 1).Split('\n');
        List<string[]> data = new List<string[]>();
        int length = line.Length;

        for (int i = 0; i < length; ++i)
        {
            string[] row = line[i].Split('\t');
            row[3] = row[3].Replace("\r", "");
            data.Add(row);
        }

        for (int i = 0; i < data.Count; ++i)
        {
            Ingredient_Item item = ScriptableObject.CreateInstance<Ingredient_Item>();
            //item = new Ingredient_Item(data[i][0], data[i][1], data[i][2], data[i][3]); 아이템 데이터 바뀜
            AssetDatabase.CreateAsset(item, "Assets/MainProjectFolder/Item/ScriptableOBj/Ingredient/Ingredient_" + item.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

}