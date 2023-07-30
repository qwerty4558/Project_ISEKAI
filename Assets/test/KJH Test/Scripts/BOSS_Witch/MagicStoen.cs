using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStoen : Enemy
{
    public int stoneIndex;
    public BOSS_Witch _witch;
    protected override void Awake()
    {
        base.Awake();
        outputName = "¸¶·Â¼®";
        _witch = FindObjectOfType<BOSS_Witch>();
    }

    protected override void Update()
    {
        if (currentHp > 0) TargetCheck();
    }

    protected override void OnEnable()
    {
        Respawn();
    }

    protected override void Respawn()
    {
        currentHp = maxHp;
    }

    protected override void GetDamage(float damage)
    {
        currentHp -= damage;
        player.OtherCheck(this);
        player.TargetOutline(this.outline);
        if(currentHp <= 0)
        {
            player.IsTarget = false;
            EnemyDead();
        }
    }

    protected override void EnemyDead()
    {
        _witch.StoneBreakCheck(stoneIndex);
        for (int i = 0; i < items.Length; i++)
        {
            GameObject temp = Instantiate(items[i], new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), Quaternion.identity);

            temp.SetActive(true);
        }
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (!other.GetComponent<ActiveAttackCol>().CompareActionType(typeof(Action_Sword))) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;

            //hitParticles.Play();
            hitAnimation.DORestartById("HIT");
            GetDamage(tempDamage);
        }
    }
}
