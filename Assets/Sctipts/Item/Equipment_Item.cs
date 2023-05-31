using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Equipment_Item", menuName = "ScriptableObjects/Equipment_Item", order = 1)]
public class Equipment_Item : Ingredient_Item
{
    [PreviewField()]
    public Sprite UI_Sprite;
    public PlayerAction AppliedAction;
    public bool belongAlways;

    public Equipment_Item(Sprite _image, string _name, string _status, string _route, int _count, int _appraiseCount) : base(_image, _name, _status, _route, _count, _appraiseCount)
    {
        this.itemImage = _image;
        this.name = _name;
        this.status = _status;
        this.count = _count;
        this.route = _route;
        this.appraiseCount = _appraiseCount;
    }
}
