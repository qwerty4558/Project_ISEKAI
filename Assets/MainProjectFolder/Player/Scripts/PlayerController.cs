using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : SingletonMonoBehaviour<PlayerController>
{
    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float rotateSpeed = 40f;
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float playerAttackDamage = 5f;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] string now_Scene;

    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject normalAttackCol; //�⺻ ��Ÿ �ݶ��̴� ���� Ű�⸸ �ؼ� ���� ����
    [SerializeField] private UIDataManager uiManager;
    [SerializeField] private InventoryTitle inven;

    private bool isAttack;

    public bool[] isClicks;
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
        isClicks[0] = true;
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        hitCollider = GetComponent<BoxCollider>();
        if(Dialog_Test != null)
            Dialog_Test.SetActive(false);
        playerSpeed = walkSpeed;
    }

    private void Update()
    {


        if (Input.GetMouseButtonDown(0) && isClicks[0] && !isClicks[1] && !isClicks[2] && !isAttack)
        {
            isAttack = true;
            animator.SetTrigger("Attack1");
        }
        if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && !isClicks[2])
        {
            isAttack = true;
            animator.SetTrigger("Attack2");
        }
        if (Input.GetMouseButtonDown(0) && isClicks[0] && isClicks[1] && isClicks[2])
        {
            isAttack = true;
            animator.SetTrigger("Attack3");
        }
        //Interaction();
        DialogTest();
        DialogTest2();
    }

    public void SetAnimCheck(int count)
    {
        isClicks[count] = true;
    }

    public void GetAnimCheck()
    {
        isAttack = false;
        isClicks[0] = true;
        isClicks[1] = false;
        isClicks[2] = false;
    }

    void FixedUpdate()
    {
        Move();
        Player_Run();
        PlayerSetAnimations();
    }

    public void Attack(float damage)
    {
        normalAttackCol.GetComponent<ActiveAttackCol>().LinkDamage = damage; // �������� ���⿡ ���� �ٸ��� �ϴ°� ������ ���߿� ��ü �ٶ�
        normalAttackCol.SetActive(true); //�����°� ���� �ݶ��̴� ������ ������
    }

    public void AttackAnimExit() //�ִϸ��̼� �߰� �� �̺�Ʈ�� ���� ����
    {

    }

    public void GetDamage(float damage)
    {
        uiManager.UpdateUI(currentHp, maxHp, true);
        currentHp -= damage;
        Debug.Log("���� ������: " + damage + " ü��: " + currentHp);
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
                //damage.Damage(playerAttackDamage);
                Debug.Log("Attack");
            }
        }
    }

    // �̵�
    private void Move()
    {
        // �Է°��� Vector3�� ����
        player_Move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        player_Move_Input.Normalize();

        // ī�޶��� Forward�� ������
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

        if(other.CompareTag("FieldItem"))
        {
            SlotItem temp = other.GetComponent<SlotItem>();
            inven.PlusItem(temp);
            Destroy(other.gameObject); //�ϴ� ����
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
