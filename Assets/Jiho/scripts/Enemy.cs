using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float enemySpeed;
    [SerializeField] private float reposSpeed;
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private float damage;
    [SerializeField] private UIDataManager uiManager;

    private bool isRePosition;
    private bool isMove;
    private bool isAttack;
    private bool isAttackDelay;
    private bool isDamage;

    private PlayerController player;
    private Animator anim;
    private Vector3 startPos;
    private Vector3 targetPos;
    
    public Camera cam;
    public Canvas canvas;

    public Vector3 StartPos { get => startPos; set => startPos = value; }
    public PlayerController Player { get => player; set => player = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isMove = true;
        currentHp = maxHp;
    }

    private void Update()
    {
        targetPos = player.transform.position;
        EnemyMove();
        CanvasMove();
    }

    private void GetDamage(float damage)
    {
        if(!isDamage)
        {
            isDamage = true;
            isMove = false;
            anim.SetBool("isMove", false);
            anim.SetTrigger("getDamage");
            currentHp -= damage;
            uiManager.UpdateUI(currentHp, maxHp, false);
        }
    }

    public void AnimExit()
    {
        isDamage = false;
        isMove = true;
    }

    private bool TargetDistance(Vector3 target, float distance)
    {
        if (Vector3.Distance(target, transform.position) < distance) return true;
        else return false;
    }

    private void CanvasMove()
    {
        canvas.transform.LookAt(canvas.transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
       
    }

    private void EnemyMove()
    {
        if (TargetDistance(targetPos, 10) && !isRePosition && !isAttack && isMove && !isDamage)
        {
            isMove = true;
            anim.SetBool("isMove", true);
            Vector3 dir = targetPos - transform.position;
            transform.forward = dir;
            transform.position = transform.position + dir * Time.deltaTime * enemySpeed;

            if (TargetDistance(targetPos, 3)) isAttack = true;
        }
        else if (isRePosition) EnemyRePos();
        else if (!isRePosition && isAttack && !isAttackDelay)
        {
            isMove = false;
            isAttack = false;
            isAttackDelay = true;
            StartCoroutine(EnemyAttack());
        }

        if(!TargetDistance(startPos, 12)) isRePosition = true;
        
    }

    private IEnumerator EnemyAttack()
    {
        //공격 애니메이션
        player.GetDamage(damage);
        yield return new WaitForSeconds(1f);
        isMove = true;
        yield return new WaitForSeconds(2f); //공격 애니메이션 생기면 그걸로 교체
        isAttackDelay = false;
    }

    private void EnemyRePos()
    {
        targetPos = startPos;
        Vector3 dir = targetPos - transform.position;
        transform.forward = dir;
        transform.position = transform.position + dir * Time.deltaTime * reposSpeed;
        currentHp = maxHp;
        if (TargetDistance(startPos, 0.5f))
        {
            isRePosition = false;
            isMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            GetDamage(tempDamage);
        }
    }
}
