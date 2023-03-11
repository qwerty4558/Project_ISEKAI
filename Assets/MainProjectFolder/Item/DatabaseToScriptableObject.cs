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

    private void OnGUI()
    {
        GUILayout.Label("Make ScriptableObject", EditorStyles.boldLabel);

        itemDatabase = EditorGUILayout.ObjectField("TXT", itemDatabase, typeof(TextAsset), false) as TextAsset;
        if (GUILayout.Button("Convert"))
        {
            ConvertScriptableObject();
        }
    }
    void ConvertScriptableObject()
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
            ItemCreator item = ScriptableObject.CreateInstance<ItemCreator>();
            item.Initialize(Convert.ToInt32(data[i][0]), data[i][1], data[i][2], data[i][3]);
            AssetDatabase.CreateAsset(item, "Assets/MainProjectFolder/Item/ScriptableOBj/Item_" + item.name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

}