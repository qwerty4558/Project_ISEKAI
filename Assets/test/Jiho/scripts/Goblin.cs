using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        EnemyMove();
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
        isAttackDelay = false;
        isRePosition = false;
        isMove = true;
        anim.SetBool("isMove", false);
    }

    protected override void EnemyMove()
    {
        base.EnemyMove();
    }

    protected override void GetDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            anim.SetTrigger("isDead");
        }
        else if(!isHit && !isAttack)
        {
            isHit = true;
            anim.SetBool("isMove", false);
            anim.SetTrigger("getDamage");
        }
    }

    public void GoblinHitAnimExit()
    {
        isHit = false;
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
