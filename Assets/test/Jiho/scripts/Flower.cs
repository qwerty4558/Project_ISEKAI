using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        outputName = "²É";
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnEnable()
    {
        Respawn();
    }

    protected override void Respawn()
    {
        transform.position = spawnPos;
        currentHp = maxHp;
    }

    protected override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    protected override void EnemyDead()
    {
        base.EnemyDead();
    }

    protected override void EnemyRePos()
    {
        if (currentHp < maxHp && !TargetDistance(targetPos.position, 10f))
        {
            currentHp = maxHp;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (!other.GetComponent<ActiveAttackCol>().CompareActionType(typeof(Action_Hand))) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            GetDamage(tempDamage);
        }
    }
}
