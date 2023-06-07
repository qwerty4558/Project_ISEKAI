using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        outputName = "½½¶óÀÓ";
    }

    protected override void Update()
    {
        if (currentHp > 0)
            TargetCheck();

        if (!isHit) Action();
    }

    protected override void Action()
    {
        anim.SetBool("isMove", false);
    }

    protected override void Idle()
    {
        base.Idle();
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

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (!other.GetComponent<ActiveAttackCol>().CompareActionType(typeof(Action_Sword))) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;

            GetDamage(tempDamage);
        }
    }
}
