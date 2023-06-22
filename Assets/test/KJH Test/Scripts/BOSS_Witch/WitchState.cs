using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WitchAnimationParam
{
    None,
    Flying,
    Attack,
    Faint
}

public class WitchState
{
    virtual public void Action(BOSS_Witch witch) { }
    virtual public void OnEnterAction(BOSS_Witch witch) { }
}

public class Witch_Idle : WitchState
{
    public override void Action(BOSS_Witch witch)
    {
        base.Action(witch);
    }
    public override void OnEnterAction(BOSS_Witch witch)
    {
        base.OnEnterAction(witch);  
    }
}

public class Witch_Flying : WitchState
{
    public override void Action(BOSS_Witch witch)
    {
        base.Action(witch);
    }
    public override void OnEnterAction(BOSS_Witch witch)
    {
        base.OnEnterAction(witch);
    }
}

public class Witch_Attack : WitchState
{
    public override void Action(BOSS_Witch witch)
    {
        base.Action(witch);
    }

    public override void OnEnterAction(BOSS_Witch witch)
    {
        base.OnEnterAction(witch);
    }

    public void Attack(PlayerController player, int attackIndex)
    {

    }
}

public class Witch_Faint : WitchState
{
    public override void Action(BOSS_Witch witch)
    {
        base.Action(witch);
    }
    public override void OnEnterAction(BOSS_Witch witch)
    {
        base.OnEnterAction(witch);
    }
}