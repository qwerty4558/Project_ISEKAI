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
    [SerializeField] private GameObject[] items;
    [SerializeField] private EnemyAttackCol enemyAttackCol;

    [SerializeField] private float time;
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
        player = FindObjectOfType<PlayerController>();
        cam = FindObjectOfType<Camera>();
        anim = GetComponent<Animator>();
        isMove = true;
        currentHp = maxHp;
        time = 15f;
        
    }

    //private int[] solution(string[] id_list, string[] report, int k)
    //{
    //    int[] answer = new int[id_list.Length];
    //    List<string> other;
    //    Dictionary<string, int> reportCount = new Dictionary<string, int>(report.Length);
    //    Dictionary<string, List<string>> who = new Dictionary<string, List<string>>(id_list.Length);
    //    string[] temp = new string[2];
    //    for(int i = 0; i < report.Length; i++)
    //    {
    //        Debug.Log(report.Length);
    //        temp = report[i].Split(" ");
    //        if (reportCount.ContainsKey(temp[1]))
    //        {
    //            reportCount[temp[1]]++;
    //        }
    //        else
    //        {
    //            reportCount.Add(temp[1], 1);
    //        }
    //        Debug.Log(temp[1] + " = " + reportCount[temp[1]]);
    //    }
    //    return answer;
    //}

    private void Update()
    {
        targetPos = player.transform.position;
        EnemyMove();
        CanvasMove();

        if(time > 0)
            time -= Time.deltaTime;
        else if(time <= 0 && !isMove)
        {
            anim.SetTrigger("isIdle");
            time = 15f;
        }
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

            if(currentHp <= 0)
            {
                for(int i = 0; i < items.Length; i++)
                {
                    GameObject temp = Instantiate(items[i], new Vector3(this.transform.position.x, 1.5f, this.transform.position.z), Quaternion.identity);
                    //temp.GetComponent<FieldItem>().Player_obj = player.gameObject;
                    temp.SetActive(true);
                }
                this.gameObject.SetActive(false);
            }
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

            if (TargetDistance(targetPos, 1)) isAttack = true;
        }
        else if (isRePosition) EnemyRePos();
        else if (!isRePosition && isAttack && !isAttackDelay)
        {
            isMove = false;
            isAttack = false;
            isAttackDelay = true;
            StartCoroutine(EnemyAttackAnimation());
        }

        if(!TargetDistance(startPos, 12)) isRePosition = true;
        
    }

    private IEnumerator EnemyAttackAnimation()
    {
        //공격 애니메이션
        anim.SetBool("isMove", false);
        anim.SetTrigger("isAttack");
        
        yield return new WaitForSeconds(2f);
        isMove = true;
        isAttackDelay = false;
    }

    public void AttackColActive()
    {
        enemyAttackCol.Damage = damage;
        enemyAttackCol.gameObject.SetActive(true);
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
            anim.SetBool("isMove", false);
            isRePosition = false;
            isMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (other.GetComponent<ActiveAttackCol>().PlayerActionState != ActionState.Sword) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            GetDamage(tempDamage);
        }
    }
}
