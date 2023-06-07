using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    protected override void Awake()
    {
        base.Awake();
        outputName = "°íºí¸°";
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void Action()
    {
        base.Action();
    }

    protected override void Idle()
    {
        base.Idle();
    }

    protected override void AttackDelay()
    {
        base.AttackDelay();
    }

    protected override void AttackColActive()
    {
        base.AttackColActive();
    }

    protected override void OnEnable()
    {
        Respawn();
    }

    protected override void Respawn()
    {
        transform.position = spawnPos;
        currentHp = maxHp;
        isAttack = false;
        isRePosition = false;
        anim.SetBool("isMove", false);
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
    }

    protected override void GetDamage(float damage)
    {
        base.GetDamage(damage);
    }

    protected override void EnemyRePos()
    {
        base.EnemyRePos();
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
