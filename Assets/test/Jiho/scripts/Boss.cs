using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//using static ToonyColorsPro.ShaderGenerator.Enums;

public class Boss : MonoBehaviour
{
    [SerializeField] private Animator bossAnimator;
    [SerializeField] private float normalAttackDamage;
    [SerializeField] private float kickAttackDamage;
    [SerializeField] private float roarAttackDamage;
    [SerializeField] private float attackDelay;
    [SerializeField] private float outlineDelay;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] private bool isMove;
    [SerializeField] private bool isAttack;
    [SerializeField] private bool isHit;
    [SerializeField] private bool isBossStart;

    [SerializeField] private ItemObject dropItem;
    [SerializeField] private DOTweenAnimation bossDotween;
    [SerializeField] private Image hpBar;
    [SerializeField] private EnemyAttackCol bossAttackCol;
    [SerializeField] private EnemyAttackCol bossRoarAttackCol;
    [SerializeField] private EnemyAttackCol bossKickAttackCol;
    [SerializeField] private GameObject hitEffect_obj;
    [SerializeField] private GameObject bossDefPos;

    private Outline outline;
    private PlayerController player;
    
    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        bossDefPos = GameObject.FindGameObjectWithTag("BossInitPos");
        bossAnimator = GetComponent<Animator>();
        outline = GetComponent<Outline>();
        currentHp = maxHp;
        BossStart();
    }

    private void Update()
    {
        if(isBossStart)
        {
            if (!isHit) Action();
            AttackDelay();
            OutlineDelay();
            HpUpdate();
        }
        
    }

    public void ResetPos()
    {
        this.transform.position = bossDefPos.transform.position;
        this.transform.LookAt(bossDefPos.transform.forward);
        currentHp = maxHp;
    }

    private void HpUpdate()
    {
        hpBar.fillAmount = currentHp / maxHp;
    }

    private void OutlineDelay()
    {
        if (outlineDelay > 0) outlineDelay -= Time.deltaTime;
        else outline.enabled = false;
    }

    private void OutlineActive()
    {
        outlineDelay = 1f;
        outline.enabled = true;
    }

    private void AttackDelay()
    {
        if(attackDelay > 0) attackDelay -= Time.deltaTime;
    }

    private bool ToPlayerDistance(float _distance) //매개변수 거리보다 가까우면 true 멀면 false
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) > _distance) return false;
        else return true;
    }

    private void Action()
    {
        if(!ToPlayerDistance(5) && !isAttack) BossMove();
        else if (ToPlayerDistance(5) && attackDelay <= 0 && !isAttack) Attack();
        else Idle();
    }

    private void BossMove()
    {
        bossAnimator.SetBool("isMove", true);
        Vector3 foward = player.transform.position - this.transform.position;
        transform.forward = foward;

        transform.position = transform.position + foward.normalized * currentSpeed * Time.deltaTime;
    }

    private void AttackRoar()
    {
        bossAnimator.SetTrigger("isRoar");
    }

    private void Idle()
    {
        bossAnimator.SetBool("isMove", false);
    }

    private void Attack()
    {
        Vector3 foward = player.transform.position - this.transform.position;
        transform.forward = foward;

        isAttack = true;
        attackDelay = 3;
        
        bossAnimator.SetBool("isMove", false);

        if (ToPlayerDistance(3))
        {
            int rand = Random.Range(0, 3);
            if (rand < 1) AttackRoar();
            else KickAttack();
        }
        else
        {
            int rand = Random.Range(0, 2);
            if (rand < 1) bossAnimator.SetTrigger("isAttack_1");
            else bossAnimator.SetTrigger("isAttack_2");
        }
    }

    private void KickAttack()
    {
        bossAnimator.SetTrigger("isKickAttack");
    }

    private void Hit()
    {
        bossDotween.DORestartById("hit");
        if (currentHp <= 0) Dead();
        else if(currentHp > 0 && !isAttack)
        {
            if(isHit)
                bossAnimator.SetTrigger("isHitExit");
            isHit = true;
            int rand = Random.Range(0, 2);
            if (rand > 0) bossAnimator.SetTrigger("isHit_1");
            else bossAnimator.SetTrigger("isHit_2");
        }
    }

    private void Dead()
    {
        bossAnimator.SetTrigger("isDead");
    }

    private void EffectActive(Transform hitPoint)
    {
        hitEffect_obj.SetActive(false);
        hitEffect_obj.transform.position = hitPoint.position;
        hitEffect_obj.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (!other.GetComponent<ActiveAttackCol>().CompareActionType(typeof(Action_Sword))) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            GetDamage(tempDamage);
            EffectActive(other.transform);
        }
    }


    public void GetDamage(float _damage)
    {
        currentHp -= _damage;
        OutlineActive();
        Hit();
    }

    public void Roar()
    {
        Vector3 foward = player.transform.position - this.transform.position;
        StartCoroutine(RoarCor(foward));
    }

    private IEnumerator RoarCor(Vector3 foward)
    {
        float count = 0;
        Rigidbody pRb = player.GetComponent<Rigidbody>();
        while(count < 0.3f)
        {
            count += Time.deltaTime;
            pRb.MovePosition(pRb.position + player.transform.forward.normalized * 25 * Time.deltaTime);
            yield return null;
            /*yield return new WaitForEndOfFrame();
            player.transform.position += foward.normalized * 25 * Time.deltaTime;*/
        }

    }

    public void HitAnimExit()
    {
        bossAnimator.SetTrigger("isHitExit");
        isHit = false;
    }

    public void AttackAnimExit()
    {
        isAttack = false;
    }

    public void DeadAnimExit()
    {
        bossDotween.DORestartById("end");
        GameObject temp = Instantiate(dropItem.gameObject, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
        temp.SetActive(true);
        this.gameObject.SetActive(false);
    
    }

    public void BossStart()
    {
        bossDotween.DORestartById("start");
        isBossStart = true;
        if (!PlayerController.instance.ControlEnabled)
        {
            PlayerController.instance.ControlEnabled = true;
        }
    }

    public void AttackCol()
    {
        bossAttackCol.Damage = normalAttackDamage;
        bossAttackCol.gameObject.SetActive(true);
    }

    public void KickAttackCol()
    {
        bossKickAttackCol.Damage = kickAttackDamage;
        bossKickAttackCol.gameObject.SetActive(true);
    }
    public void RoarAttackCol()
    {
        bossRoarAttackCol.Damage = roarAttackDamage;
        bossRoarAttackCol.gameObject.SetActive(true);
    }
}
