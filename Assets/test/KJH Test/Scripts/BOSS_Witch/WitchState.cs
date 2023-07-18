using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WITCH_STATES
{
    Phase = 1,
    Faint
}

public class WitchState
{
    virtual public void Action(BOSS_Witch witch) { }
    virtual public void OnEnterAction(BOSS_Witch witch) { }
}

public class Witch_Phase : WitchState
{
    override public void Action(BOSS_Witch witch)
    {
        
    }

    override public void OnEnterAction(BOSS_Witch witch) 
    {

    }
}

public class Witch_Faint : WitchState
{
    public override void Action(BOSS_Witch witch)
    {
        
    }
    public override void OnEnterAction(BOSS_Witch witch)
    {
        
    }
}