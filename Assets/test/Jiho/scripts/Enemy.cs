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
    protected bool isMove;
    protected bool isRePosition;
    protected bool isAttack;
    protected bool isAttackDelay;

    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected Transform targetPos;
    [SerializeField] protected GameObject[] items;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Camera cam;
    [SerializeField] protected Canvas canvas;

    [SerializeField] protected UIDataManager uiManager;
    [SerializeField] protected EnemyAttackCol enemyAttackCol;
    [SerializeField] protected PlayerController player;


    protected virtual void OnEnable()
    {

    }

    protected virtual void Awake()
    {
        currentHp = maxHp;
        isMove = true;
        player = FindObjectOfType<PlayerController>();
        spawnPos = transform.position;
        cam = FindObjectOfType<Camera>();
        canvas.worldCamera = cam;
    }

    protected virtual void Update()
    {
        
        CanvasMove();
    }

    protected virtual void Respawn()
    {
        
    }

    protected virtual void EnemyMove()
    {
        targetPos = player.transform;
        if (TargetDistance(targetPos.position, 7) && !isRePosition && !isAttack && isMove)
        {
            isMove = true;
            anim.SetBool("isMove", true);
            Vector3 dir = targetPos.position - transform.position;
            transform.forward = dir;
            transform.position = transform.position + dir.normalized * Time.deltaTime * enemySpeed;

            if (TargetDistance(targetPos.position, 2)) isAttack = true;
        }
        else if (isRePosition) EnemyRePos();
        else if (!isRePosition && isAttack && !isAttackDelay)
        {
            isMove = false;
            isAttack = false;
            isAttackDelay = true;
            StartCoroutine(EnemyAttackAnimation());
        }

        if (!TargetDistance(spawnPos, 5)) isRePosition = true;
    }

    protected virtual void CanvasMove()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);

    }

    protected virtual void GetDamage(float damage)
    {
        currentHp -= damage;
        uiManager.UpdateUI(currentHp, maxHp);

        if (currentHp <= 0)
        {

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
            GameObject temp = Instantiate(items[i], new Vector3(this.transform.position.x, 1.5f, this.transform.position.z), Quaternion.identity);

            temp.SetActive(true);
        }
        this.gameObject.SetActive(false);
    }

    protected virtual void EnemyRePos()
    {
        Vector3 dir = spawnPos - transform.position;
        transform.forward = dir;
        transform.position = transform.position + dir * Time.deltaTime * reposSpeed;
        currentHp = maxHp;
        uiManager.UpdateUI(currentHp, maxHp);
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