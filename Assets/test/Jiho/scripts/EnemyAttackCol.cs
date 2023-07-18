using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCol : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float activeTime;

    public float Damage { get => damage; set => damage = value; }

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
