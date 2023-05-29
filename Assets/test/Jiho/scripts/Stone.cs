using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        outputName = "Ã¶±¤¼®";
    }

    protected override void Update()
    {
        if (currentHp > 0)
            TargetCheck();
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
        currentHp -= damage;
        player.OtherCheck(this);
        player.TargetOutline(this.outline);

        if (currentHp <= 0)
        {
            player.IsTarget = false;
            EnemyDead();
        }
    }

    protected override void EnemyDead()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject temp = Instantiate(items[i], new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), Quaternion.identity);

            temp.SetActive(true);
        }
        EnemySpawner.instance.GetEnemyData(enemyName, false);
        this.gameObject.SetActive(false);
    }

    protected override void EnemyRePos()
    {
        if (currentHp < maxHp && !ToPlayerDistance(playerCheckRange))
        {
            currentHp = maxHp;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (!other.GetComponent<ActiveAttackCol>().CompareActionType(typeof(Action_Pickaxe))) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            GetDamage(tempDamage);
        }
    }
}
