using System;
using UnityEngine;
using PlayerInterface;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class PlayerController : SerializedMonoBehaviour
{

    public static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PlayerController();
                if(instance == null)
                {
                    GameObject gobj = new GameObject();
                    gobj.name = typeof(PlayerController).Name;
                    instance = gobj.AddComponent<PlayerController>();
                }                
            }
            return instance;
        }
        
    }

    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float playerAttackDamage = 5f;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject normalAttackCol; //기본 평타 콜라이더 껏다 키기만 해서 공격 판정
    [SerializeField] private UIDataManager uiManager;
    [SerializeField] private CameraFollow cameraFollow;
    

    public GameObject sword_obj;
    public GameObject pickaxe_obj;
    public GameObject axe_obj;

    [SerializeField] private List<PlayerAction> playerActions;
    private int currentActionIndex = 0;

    private bool isAttack;
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }

    public InventoryTitle inven;
    public bool[] isClicks;
    private Animator animator;
    public Animator anim { get { return animator; }}

    public bool ControlEnabled = true;
    [SerializeField] private LayerMask interactableLayermask;

   
    BoxCollider hitCollider;
    //[SerializeField] float dashSpeed = 7f;

    Vector3 player_Move_Input;

    Vector3 heading;

    bool isMove = false;
    bool isRun = false;
    bool idleB = false;

    [SerializeField] float idleChangeTime = 5.5f;

    [SerializeField] AudioSource abs;
    [SerializeField] AudioClip player_Interction_SFX;

    SoundModule soundModule;
    public SoundModule SoundModule { get { return soundModule; }}

    private void Awake()
    {
        if(instance == null)
        {
            instance = this as PlayerController;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isClicks[0] = true;
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        hitCollider = GetComponent<BoxCollider>();  
        soundModule = GetComponent<SoundModule>();
        playerSpeed = walkSpeed;
        UI_Tools tool = (UI_Tools)FindObjectOfType(typeof(UI_Tools));
        if(tool != null)
        tool.SwitchCurrentTool(playerActions.ToArray(),currentActionIndex);
        abs = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (ControlEnabled)
        {
            if (!cameraFollow.isInteraction)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (playerActions != null)
                    {
                        playerActions[currentActionIndex].Action(this);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currentActionIndex > 0)
                {
                    currentActionIndex--;
                    ChangeAction(playerActions[currentActionIndex]);
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentActionIndex + 1 < playerActions.Count)
                {
                    currentActionIndex++;
                    ChangeAction(playerActions[currentActionIndex]);
                }
            }
        }

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
        if (!cameraFollow.isInteraction && ControlEnabled)
        {
            Move();
            Player_Run();
            PlayerSetAnimations();
        }

        if(ControlEnabled)
        {
            Vector3 counterCamera = Vector3.ProjectOnPlane(transform.position - Camera.main.transform.position,Vector3.up).normalized;
            Ray interactionRay = new Ray(transform.position + Vector3.up, counterCamera);
            Debug.DrawRay(interactionRay.origin, interactionRay.origin + counterCamera*interactionRange);
            var rHits = Physics.RaycastAll(interactionRay, interactionRange,interactableLayermask);

            if (rHits.Length != 0)
            {
                UI_Interaction interactionUI = FindObjectOfType<UI_Interaction>();
                var targetInteraction = rHits[0].collider.GetComponent<InteractableObject>();

                if (interactionUI != null && targetInteraction != null)
                {
                    interactionUI.SetInteractionUI(targetInteraction);

                    if (Input.GetKeyDown(KeyCode.F)) targetInteraction.OnInteract();
                }
            }
            else
            {
                UI_Interaction interactionUI = FindObjectOfType<UI_Interaction>();
                if(interactionUI != null)
                interactionUI.Disable();
            }
        }
    }

    public void Attack()
    {
        normalAttackCol.GetComponent<ActiveAttackCol>().LinkDamage = playerAttackDamage; // 데미지는 무기에 따라 다르게 하는게 나으니 나중에 교체 바람
        normalAttackCol.SetActive(true); //꺼지는건 공격 콜라이더 스스로 꺼지게
        normalAttackCol.GetComponent<ActiveAttackCol>().currentPlayerAction = playerActions[currentActionIndex];
    }

    public void AttackAnimExit() //애니메이션 추가 후 이벤트로 적용 예정
    {

    }

    public void AttackAction()
    {
        if (isClicks[0] && !isClicks[1] && !isClicks[2] && !isAttack)
        {
            isAttack = true;
            animator.SetTrigger("Attack1");
        }
        if (isClicks[0] && isClicks[1] && !isClicks[2])
        {
            isAttack = true;
            animator.SetTrigger("Attack2");
        }
        if (isClicks[0] && isClicks[1] && isClicks[2])
        {
            isAttack = true;
            animator.SetTrigger("Attack3");
        }
    }

    public void GetDamage(float damage)
    {
        uiManager.UpdateUI(currentHp, maxHp);
        currentHp -= damage;
        Debug.Log("맞은 데미지: " + damage + " 체력: " + currentHp);
    }

    public void ChangeAction(PlayerAction action)
    {
        action.OnEnterAction(this);

        if (FindObjectOfType(typeof(UI_Tools)))
        {
            UI_Tools tool = (UI_Tools)FindObjectOfType(typeof(UI_Tools));

            tool.SwitchCurrentTool(playerActions.ToArray(),currentActionIndex);
        }
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

        if (player_Move_Input != Vector3.zero && !isAttack)
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
            //animator.SetBool("IdleB", idleB);
            animator.SetTrigger("IdleAlt");
        }

        animator.SetBool("isWalk", isMove);
        animator.SetBool("isRun", isRun);
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("EnemyAttackCol"))
        {
            Debug.Log("공격 받음 : " + other.GetComponent<EnemyAttackCol>().Damage);
            GetDamage(other.GetComponent<EnemyAttackCol>().Damage);
        }

        if(other.CompareTag("QuestPos"))
        {
            QuestTitle.instance.QuestPositionCheck(other.name);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("NPC"))
        {

        }
    }

    public void SavePlayerStates()
    {

    }
    public void LoadPlayerStates()
    {

    }
}