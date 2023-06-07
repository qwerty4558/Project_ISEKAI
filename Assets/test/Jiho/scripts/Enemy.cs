using Cinemachine.Utility;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float maxHp;
    [SerializeField] protected float currentHp;
    [SerializeField] protected float damage;
    [SerializeField] protected float playerCheckRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float respawnRange;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected string enemyName;
    [SerializeField] protected bool isRePosition;
    [SerializeField] protected bool isAttack;
    [SerializeField] protected bool isHit;

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
        currentHp = maxHp;
        player = FindObjectOfType<PlayerController>();
        spawnPos = transform.position;
       
    }

    protected virtual void Update()
    {
        if(currentHp > 0)
            TargetCheck();

        if (!isHit) Action();
        AttackDelay();


    }

    protected virtual void Action()
    {
        if (!ToSpawnDistance(respawnRange) || isRePosition) EnemyRePos();
        else if (ToPlayerDistance(attackRange) && !isAttack && !isRePosition && attackDelay <= 0) Attack();
        else if (ToPlayerDistance(playerCheckRange) && !ToPlayerDistance(attackRange) && !isAttack && !isRePosition && !isHit) EnemyMove();
        else Idle();
    }

    protected virtual void AttackDelay()
    {
        if (attackDelay > 0) attackDelay -= Time.deltaTime;
    }

    protected virtual void Attack()
    {
        Vector3 foward = player.transform.position - this.transform.position;
        transform.forward = foward;

        isAttack = true;
        attackDelay = 3;

        anim.SetBool("isMove", false);
        anim.SetTrigger("isAttack");
    }

    protected virtual void EnemyMove()
    {
        anim.SetBool("isMove", true);
        Vector3 foward = player.transform.position - this.transform.position;
        transform.forward = foward;

        transform.position = transform.position + foward.normalized * enemySpeed * Time.deltaTime;

    }

    protected virtual void Idle()
    {
        anim.SetBool("isMove", false);
    }

    protected virtual bool ToPlayerDistance(float _distance) //매개변수 거리보다 가까우면 true 멀면 false
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) > _distance) return false;
        else return true;
    }

    protected virtual bool ToSpawnDistance(float _distance)
    {
        if (Vector3.Distance(spawnPos, this.transform.position) > _distance) return false;
        else return true;
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
        else if(currentHp > 0 && !isAttack)
        {
            anim.SetBool("isMove", false);
            if (isHit) anim.SetTrigger("getDamageExit");
            isHit = true;
            anim.SetTrigger("getDamage");
        }
    }

    protected virtual void AttackColActive()
    {
        enemyAttackCol.Damage = damage;
        enemyAttackCol.gameObject.SetActive(true);
    }

    public void AttackAnimExit()
    {
        isAttack = false;
    }

    public void GetDamageAnimExit()
    {
        anim.SetTrigger("getDamageExit");
        isHit = false;
    }

    public void DeadAnimExit()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject temp = Instantiate(items[i], new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z), Quaternion.identity);

            temp.SetActive(true);
        }
        EnemySpawner.instance.GetEnemyData(enemyName, false);
        this.gameObject.SetActive(false);
    }

    protected virtual void EnemyDead()
    {
        anim.SetTrigger("isDead");
    }

    protected virtual void EnemyRePos()
    {
        isRePosition = true;

        Vector3 forward = spawnPos - transform.position;
        transform.forward = forward;
        transform.position = transform.position + forward.normalized * 3 * Time.deltaTime;
        currentHp = maxHp;
        if (ToSpawnDistance(0.5f))isRePosition = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }

}