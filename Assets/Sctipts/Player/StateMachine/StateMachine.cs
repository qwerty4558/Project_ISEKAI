using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State currentState;
    protected bool inTransition;
    public virtual State CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            Transition(value);
        }
    }
    protected virtual T GetState<T>() where T : State
    {
        T target = GetComponent<T>();

        if(target == null) 
        {
            target= GetComponent<T>();
        }
        return target;
    }

    protected virtual void ChangeState<T> () where T : State
    {
        CurrentState = GetComponent<T>();
    }

    protected virtual void Transition(State value)
    {
        if (currentState == value || inTransition) return;
        inTransition = true;

        if(currentState != null) currentState.Exit();
        currentState = value;

        if (currentState != null) currentState.Enter();
        inTransition = false;
    }
}
