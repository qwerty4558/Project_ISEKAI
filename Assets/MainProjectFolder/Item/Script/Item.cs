using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class Item 
{
    public int item_type;
    public int mat_item1;
    public int mat_item2;
    public int item_id;
    public string item_name;

    public Item(int item_type, int mat_item1, int mat_item2, int item_id, string item_name)
    {
        this.item_type = item_type;
        this.mat_item1 = mat_item1;
        this.mat_item2 = mat_item2;
        this.item_id = item_id;
        this.item_name = item_name;
    }
}
