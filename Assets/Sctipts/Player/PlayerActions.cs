using PlayerInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction
{
    public Sprite itemSprite;

    virtual public void Action(PlayerController player) { }
    virtual public void OnEnterAction(PlayerController player) { }

    public PlayerAction(Sprite _itemSprite) 
    { 
        itemSprite = _itemSprite;
    }
}

[System.Serializable]
public class Action_Hand : PlayerAction
{
    public Action_Hand(Sprite _itemSprite) : base(_itemSprite)
    {
    }


    public override void Action(PlayerController player)
    {
        player.IsAttack = true;
        player.anim.SetTrigger("hand");
    }

    public override void OnEnterAction(PlayerController player)
    {
        player.pickaxe_obj.SetActive(false);
        player.axe_obj.SetActive(false);
        player.sword_obj.SetActive(false);
    }
}

[System.Serializable]
public class Action_Sword : PlayerAction
{
    public Action_Sword(Sprite _itemSprite) : base(_itemSprite)
    {
    }


    public override void Action(PlayerController player)
    {
        player.IsAttack = true;
        player.anim.SetTrigger("Attack1");
    }

    public override void OnEnterAction(PlayerController player)
    {
        player.pickaxe_obj.SetActive(false);
        player.axe_obj.SetActive(false);
        player.sword_obj.SetActive(true);
    }
}

[System.Serializable]
public class Action_Pickaxe : PlayerAction
{
    public Action_Pickaxe(Sprite _itemSprite) : base(_itemSprite)
    {
    }


    public override void Action(PlayerController player)
    {
        player.IsAttack = true;
        player.anim.SetTrigger("pickaxe");
    }

    public override void OnEnterAction(PlayerController player)
    {
        player.pickaxe_obj.SetActive(true);
        player.axe_obj.SetActive(false);
        player.sword_obj.SetActive(false);
    }
}

[System.Serializable]
public class Action_Axe : PlayerAction
{
    public Action_Axe(Sprite _itemSprite) : base(_itemSprite)
    {

    }


    public override void Action(PlayerController player)
    {
        player.IsAttack = true;
        player.anim.SetTrigger("axe");
    }

    public override void OnEnterAction(PlayerController player)
    {
        player.pickaxe_obj.SetActive(false);
        player.axe_obj.SetActive(true);
        player.sword_obj.SetActive(false);
    }
}