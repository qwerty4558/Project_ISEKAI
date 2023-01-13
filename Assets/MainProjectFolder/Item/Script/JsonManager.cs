using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    public List<Item> myItemList = new List<Item>();

    void ParsingJsonItem(JsonData name, List<Item> listItem)
    {
        for(int i = 0; i< listItem.Count; i++)
        {
            string tempItemType = name[i][0].ToString();
            string tempMatItem1 = name[i][1].ToString();
            string tempMatItem2 = name[i][2].ToString();
            string tempItemID = name[i][3].ToString();
            string tempItemName = name[i][4].ToString();

            int _tempItemType = int.Parse(tempItemType);
            int _tempMatItem1 = int.Parse(tempMatItem1);
            int _tempMatItem2 = int.Parse(tempMatItem2);
            int _tempItemID = int.Parse(tempItemID);

            Item tempItem = new Item(_tempItemType, _tempMatItem1, _tempMatItem2, _tempItemID, tempItemName);
            itemList.Add(tempItem);
            Debug.Log("Item Parsing Access!");
        }
    }

    public void LoadBase()
    {
        string jsonstring;
        string filepath;

        filepath = Application.streamingAssetsPath + "/itemData.json";
    }
}
