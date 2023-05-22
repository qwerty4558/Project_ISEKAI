using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float reposSpeed;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float playerCheckRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float respawnRange;
    [SerializeField] protected string enemyName;
    protected bool isMove;
    protected bool isRePosition;
    protected bool isAttack;
    protected bool isAttackDelay;
    protected bool isHit;

    [HideInInspector]
    public string outputName;

    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected Transform targetPos;
    [SerializeField] protected GameObject[] items;
    [SerializeField] protected Animator anim;

    [SerializeField] protected EnemyAttackCol enemyAttackCol;
    [SerializeField] protected PlayerController player;
    [SerializeField] protected Outline outline;


    public string EnemyName { get => enemyName; }
    public float CurrentHp { get => currentHp; }
    public float MaxHp { get => maxHp; }

    protected virtual void OnEnable()
    {

    }

    protected virtual void Awake()
    {
        playerCheckRange = 7;
        attackRange = 2;
        respawnRange = 5;
        currentHp = maxHp;
        isMove = true;
        player = FindObjectOfType<PlayerController>();
        spawnPos = transform.position;
    }

    protected virtual void Update()
    {
        if(currentHp > 0)
            TargetCheck();
    }

    protected virtual void TargetCheck()
    {
        if(!player.IsTarget)
        {
            if(Vector3.Distance(player.transform.position, this.transform.position) <= 3f)
            {
                player.IsTarget = true;
                player.TargetOutline(outline);
            }
        }

        if(outline.enabled == true)
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) > 3f)
            {
                player.IsTarget = false;
                outline.enabled = false;
            }
        }
    }

    protected virtual void Respawn()
    {
        
    }

    protected virtual void EnemyMove()
    {
        targetPos = player.transform;
        if (TargetDistance(targetPos.position, playerCheckRange) && !isRePosition && !isAttack && isMove && !isHit)
        {
            isMove = true;
            anim.SetBool("isMove", true);
            Vector3 dir = targetPos.position - transform.position;
            transform.forward = dir;
            transform.position = transform.position + dir.normalized * Time.deltaTime * enemySpeed;

            if (TargetDistance(targetPos.position, attackRange)) isAttack = true;
        }
        else if (isRePosition) EnemyRePos();
        else if (!isRePosition && isAttack && !isAttackDelay)
        {
            isMove = false;
            isAttack = false;
            isAttackDelay = true;
            StartCoroutine(EnemyAttackAnimation());
        }

        if (!TargetDistance(spawnPos, respawnRange)) isRePosition = true;
    }

    protected virtual void GetDamage(float damage)
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

    protected IEnumerator EnemyAttackAnimation()
    {
        //공격 애니메이션
        anim.SetBool("isMove", false);
        anim.SetTrigger("isAttack");

        yield return new WaitForSeconds(2f);
        isMove = true;
        isAttackDelay = false;
    }

    protected void AttackColActive()
    {
        enemyAttackCol.Damage = damage;
        enemyAttackCol.gameObject.SetActive(true);
    }

    protected bool TargetDistance(Vector3 target, float distance)
    {
        if (Vector3.Distance(target, transform.position) < distance) return true;
        else return false;
    }

    protected virtual void EnemyDead()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject temp = Instantiate(items[i], new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), Quaternion.identity);

            temp.SetActive(true);
        }
        EnemySpawner.instance.GetEnemyData(enemyName, false);
        this.gameObject.SetActive(false);
    }

    protected virtual void EnemyRePos()
    {
        Vector3 dir = spawnPos - transform.position;
        transform.forward = dir;
        transform.position = transform.position + dir * Time.deltaTime * reposSpeed;
        currentHp = maxHp;
        if (TargetDistance(spawnPos, 0.5f))
        {
            anim.SetBool("isMove", false);
            isRePosition = false;
            isMove = true;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }

}