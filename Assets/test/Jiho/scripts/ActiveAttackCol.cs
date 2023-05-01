using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAttackCol : MonoBehaviour
{
    [SerializeField] private float activeTime;
    private float linkDamage;

    public float LinkDamage { get => linkDamage; set => linkDamage = value; }

    public PlayerAction currentPlayerAction;

    public bool CompareActionType(Type type)
    {
        return currentPlayerAction.GetType() == type;
    }

    private void OnEnable()
    {
        StartCoroutine(ActiveFalseCol());
    }

    private IEnumerator ActiveFalseCol()
    {
        yield return new WaitForSeconds(activeTime);
        this.gameObject.SetActive(false);
    }
}
