using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
   public virtual void Enter()
    {
        AddListeners();
    }

    public virtual void Exit()
    {
        RemoveListners();
    }

    public virtual void OnDestroy()
    {
        RemoveListners();
    }

    public virtual void AddListeners()
    {

    }

    public virtual void RemoveListners()
    {

    }
}
