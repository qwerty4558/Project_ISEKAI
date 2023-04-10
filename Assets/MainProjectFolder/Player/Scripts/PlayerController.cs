using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float rotateSpeed = 40f;
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float playerAttackDamage = 1f;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] string now_Scene;

    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject normalAttackCol; //기본 평타 콜라이더 껏다 키기만 해서 공격 판정
    [SerializeField] private UIDataManager uiManager;

    public GameObject BoardText;
    Animator animator;

    public GameObject Dialog_Test;


    BoxCollider hitCollider;
    //[SerializeField] float dashSpeed = 7f;

    Vector3 player_Move_Input;

    Vector3 heading;

    bool isMove = false;
    bool isRun = false;

    bool idleB = false;

    [SerializeField] float idleChangeTime = 5.5f;


    void Start()
    {
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        hitCollider = GetComponent<BoxCollider>();
        if(Dialog_Test != null)
            Dialog_Test.SetActive(false);
        playerSpeed = walkSpeed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Attack();
            //Interaction();
        DialogTest();
        DialogTest2();
    }

    void FixedUpdate()
    {
        Move();
        Player_Run();
        PlayerSetAnimations();
    }

    private void Attack()
    {
        normalAttackCol.GetComponent<ActiveAttackCol>().LinkDamage = playerAttackDamage; // 데미지는 무기에 따라 다르게 하는게 나으니 나중에 교체 바람
        normalAttackCol.SetActive(true); //꺼지는건 공격 콜라이더 스스로 꺼지게 할 예정
    }

    private void AttackDelay() //애니메이션 추가 후 이벤트로 적용 예정
    {

    }

    public void GetDamage(float damage)
    {
        uiManager.UpdateUI(currentHp, maxHp, true);
        currentHp -= damage;
        Debug.Log("맞은 데미지: " + damage + " 체력: " + currentHp);
    }

    private void Interaction()
    {
        Ray ray = new Ray
        {
            origin = transform.position,
            direction = transform.forward
        };

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            IPlayerAction.IDamage damage = hit.collider.GetComponent<IPlayerAction.IDamage>();
            if (damage != null)
            {
                damage.Damage(playerAttackDamage);
                Debug.Log("Attack");
            }
        }
    }

    // 이동
    private void Move()
    {
        // 입력값을 Vector3에 저장
        player_Move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        player_Move_Input.Normalize();

        // 카메라의 Forward를 가져옴
        heading = Camera.main.transform.forward;
        heading.y = 0;
        heading.Normalize();

        heading = heading - player_Move_Input;

        if (player_Move_Input != Vector3.zero)
        {
            isMove = true;
            
            float angle = Mathf.Atan2(heading.z, heading.x) * Mathf.Rad2Deg * -2;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * rotateSpeed);

            transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed);
        }
        else
        {
            isMove = false;
        }
    }

    private void Player_Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isMove)
        {
            playerSpeed = runSpeed;
            isRun = true;
        }
        else
        {
            playerSpeed = walkSpeed;
            isRun = false;
        }
    }
    private void PlayerSetAnimations()
    {
        if (!isRun && !isMove)
        {
            idleChangeTime -= Time.deltaTime;
            if (idleChangeTime <= 0)
            {
                idleB = !idleB;
                idleChangeTime = 5.5f;
            }
            animator.SetBool("IdleB", idleB);
        }

        animator.SetBool("isWalk", isMove);
        animator.SetBool("isRun", isRun);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("QuestBoard"))
        {
            BoardText.SetActive(true);
        }
    }

    private void DialogTest()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Dialog_Test.SetActive(true);
        }
    }
    private void DialogTest2()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Dialog_Test.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("QuestBoard"))
        {
            BoardText.SetActive(false);
        }
    }
}
