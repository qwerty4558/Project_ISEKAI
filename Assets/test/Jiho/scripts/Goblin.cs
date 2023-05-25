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
        player.OtherCheck(this);
        player.TargetOutline(this.outline);

        if (currentHp <= 0)
        {
            player.IsTarget = false;
            anim.SetTrigger("isDead");
        }
        else if(!isAttack)
        {
            isHit = true;
            anim.SetTrigger("getDamageExit");
            anim.SetTrigger("getDamage");
        }
    }

    public void GoblinHitAnimExit()
    {
        anim.SetTrigger("getDamageExit");
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
