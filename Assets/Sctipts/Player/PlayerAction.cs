using PlayerInterface;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerAnimParameter
{
    None,
    Sword,
    Pickaxe,
    Axe
}

public class PlayerAction
{
    public Equipment_Item assignedEquipmentData;
    public AnimatorOverrideController animOverride;
    protected bool isAttack = false;
    virtual public void Action(PlayerController player) { }
    virtual public void OnEnterAction(PlayerController player) { }
}

[System.Serializable]
public class Action_Hand : PlayerAction
{
    public Action_Hand(Sprite _itemSprite)
    {
    }


    public override void Action(PlayerController player)
    {
        if (player.IsAttack) return;
        player.IsAttack = true;
        player.anim.SetTrigger("Action");
        player.SoundModule.Play("Action_Hand");

    }

    public override void OnEnterAction(PlayerController player)
    {
        if (animOverride != null)
            player.anim.runtimeAnimatorController = animOverride;

        player.anim.SetInteger("CurrentAction", (int)PlayerAnimParameter.None);
        player.pickaxe_obj.SetActive(false);
        player.axe_obj.SetActive(false);
        player.sword_obj.SetActive(false);
    }
}

[System.Serializable]
public class Action_Sword : PlayerAction
{
    public Action_Sword(Sprite _itemSprite)
    {
    }


    public override void Action(PlayerController player)
    {
        player.AttackAction();
        //player.IsAttack = true;
        //player.anim.SetTrigger("Action");
        //player.SoundModule.Play("Action_Sword");
    }

    public override void OnEnterAction(PlayerController player)
    {
        if (animOverride != null)
            player.anim.runtimeAnimatorController = animOverride;

        player.anim.SetInteger("CurrentAction", (int)PlayerAnimParameter.Sword);
        player.pickaxe_obj.SetActive(false);
        player.axe_obj.SetActive(false);
        player.sword_obj.SetActive(true);
    }
}

[System.Serializable]
public class Action_Pickaxe : PlayerAction
{
    public Action_Pickaxe(Sprite _itemSprite)
    {
    }

    public override void Action(PlayerController player)
    {
        if (player.IsAttack) return;
        player.IsAttack = true;
        player.anim.SetTrigger("Action");
        player.SoundModule.Play("Action_Pickaxe");
    }

    public override void OnEnterAction(PlayerController player)
    {
        if (animOverride != null)
            player.anim.runtimeAnimatorController = animOverride;

        player.anim.SetInteger("CurrentAction", (int)PlayerAnimParameter.Pickaxe);
        player.pickaxe_obj.SetActive(true);
        player.axe_obj.SetActive(false);
        player.sword_obj.SetActive(false);
    }


}

[System.Serializable]
public class Action_Axe : PlayerAction
{
    public Action_Axe(Sprite _itemSprite)
    {

    }


    public override void Action(PlayerController player)
    {
        if (player.IsAttack) return;
        player.IsAttack = true;
        player.anim.SetTrigger("Action");
        player.SoundModule.Play("Action_Axe");
    }

    public override void OnEnterAction(PlayerController player)
    {
        if (animOverride != null)
            player.anim.runtimeAnimatorController = animOverride;

        player.anim.SetInteger("CurrentAction", (int)PlayerAnimParameter.Axe);
        player.pickaxe_obj.SetActive(false);
        player.axe_obj.SetActive(true);
        player.sword_obj.SetActive(false);
    }
}