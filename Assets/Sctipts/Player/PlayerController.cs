using System;
using UnityEngine;
using PlayerInterface;
using System.Collections.Generic;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using System.Reflection;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerController : SerializedMonoBehaviour
{

    public static PlayerController instance;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerController();
                if (instance == null)
                {
                    GameObject gobj = new GameObject();
                    gobj.name = typeof(PlayerController).Name;
                    instance = gobj.AddComponent<PlayerController>();
                }
            }
            return instance;
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLoadScene;
    }
    

    private void OnLoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        
    }

    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float runSpeed = 7f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float playerAttackDamage = 5f;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject normalAttackCol; //?? ??? ?????? ???? ??? ??? ???? ????
    [SerializeField] private UIDataManager uiManager;
    [SerializeField] private GameObject otherHp_obj;
    [SerializeField] private Image playerHp_Bar;
    [SerializeField] private UnityEngine.UI.Outline playerHP_Outline;
    [SerializeField] private ParticleSystem attackParticle;
   // [SerializeField] PlayerStateInfomation playerInfo; // 플레이어의 상태를 저장하는 공간

    Rigidbody rd;
    //[SerializeField] private ParticleSystem[] player_Attack_VFX;
    //[SerializeField] private ParticleSystem player_Hit_VFX;

    public CameraFollow cameraFollow;
    public GameObject sword_obj;
    public GameObject pickaxe_obj;
    public GameObject axe_obj;
    public Outline targetOutline;
    
    public float HP { get => currentHp; set => currentHp = value; }

    [SerializeField] private List<PlayerAction> playerActions;
    public List<PlayerAction> PlayerActions { get { return playerActions; } }
    [SerializeField] private UI_Tools tool;

    private int currentActionIndex = 0;

    [SerializeField] private bool isTarget;

    [SerializeField] float desiredGravityForce;
    private bool isAttack;
    public bool IsAttack { get { return isAttack; } set { isAttack = value; } }
    public bool IsTarget { get => isTarget; set => isTarget = value; }
    public InventoryTitle inven;
    public bool isClicks;
    private Animator animator;
    public Animator anim { get { return animator; } }
    public TextMeshProUGUI otherName;
    public Image otherHp;

    public bool ControlEnabled = true;
    [SerializeField] private LayerMask interactableLayermask;

    private float hpTime;
    private float enemyMaxHP;
    private float enemyCurrentHP;

    BoxCollider hitCollider;
    //[SerializeField] float dashSpeed = 7f;

    Vector3 player_Move_Input;

    Vector3 heading;

    bool isMove = false;
    bool isRun = false;
    bool idleB = false;

    [SerializeField] float idleChangeTime = 5.5f;

    [SerializeField] public AudioSource abs;
    [SerializeField] AudioClip player_Interction_SFX;

    SoundModule soundModule;
    public SoundModule SoundModule { get { return soundModule; } }

    [FoldoutGroup("Actions")]
    public UnityEvent OnStep;
    [FoldoutGroup("Actions")]
    public UnityEvent OnAttack;

    [FoldoutGroup("GameOver")]
    public UnityEvent OnGameOver;


    [SerializeField] DOTweenAnimation amountShake;
    [SerializeField] DOTweenAnimation enemy_HP_Shake;
    private void Awake()
    {
        if (instance == null)
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
        isClicks = true;
        currentHp = maxHp;
        animator = GetComponent<Animator>();
        hitCollider = GetComponent<BoxCollider>();
        soundModule = GetComponent<SoundModule>();
        rd = GetComponent<Rigidbody>();
        playerSpeed = walkSpeed;
        playerHP_Outline.enabled = false;

        tool = FindObjectOfType<UI_Tools>();
        if (tool != null)
        {
            SetValidPlayerActions();
            tool.SwitchCurrentTool(playerActions.ToArray(), 0);
        }
        desiredGravityForce = -100f;

        abs = GetComponent<AudioSource>();

        StopAllCoroutines();
    }

    private void Update()
    {
        InputInteraction();
        OtherHpSetActive();
        CheckGameOver();
        FillAmountHP();
        OtherFillAmount();

        
    }

    private void CheckGameOver()
    {
        if (currentHp > 0) return;
        else
        {
            OnGameOver.Invoke();
            currentHp = maxHp;
            //SceneInfomation.Instance.ReSpawnPlayer();
        }
    }

    private void InputInteraction()
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
                if (currentActionIndex < playerActions.Count - 1)
                {
                    currentActionIndex++;
                    ChangeAction(playerActions[currentActionIndex]);

                }
            }
        }
    }

    private void OtherHpSetActive()
    {
        if (hpTime > 0)
        {
            otherHp_obj.SetActive(true);
  
            hpTime -= Time.deltaTime;
        }
        else
        {
            if (otherHp_obj.activeSelf)
                otherHp_obj.SetActive(false);
        }
    }

    public void OtherCheck(Enemy enemy)
    {
        
        otherName.text = enemy.outputName;
        enemyCurrentHP = enemy.CurrentHp;
        enemyMaxHP = enemy.MaxHp;
        hpTime = 1.5f;
    }

    private void OtherFillAmount()
    {
        if (otherHp_obj.activeSelf)
        {
            float fillSpeed = 5f;
            otherHp.fillAmount = Mathf.Lerp(otherHp.fillAmount, enemyCurrentHP / enemyMaxHP, fillSpeed * Time.deltaTime);
        }
        else
        {
            if (enemyCurrentHP > 0) return;
            else otherHp.fillAmount = 1f;
        }
    }

    public void TargetOutline(Outline outline)
    {
        if (targetOutline != null)
        {
            targetOutline.enabled = false;
            targetOutline = outline;
            targetOutline.enabled = true;
        }
        else if (targetOutline == null)
        {
            targetOutline = outline;
            targetOutline.enabled = true;
        }
    }

    public void SetAnimCheck(int count)
    {
        isClicks = true;
    }

    public void GetAnimCheck()
    {
        isAttack = false;
    }


    void FixedUpdate()
    {
        if (currentHp < (maxHp / 2))
        {
            playerHP_Outline.enabled = true;
            StartCoroutine(IBlink_HP_Bar());

        }
        else
        {
            StopCoroutine(IBlink_HP_Bar());
        }
        if (!cameraFollow.isInteraction && ControlEnabled)
        {
            Move();
            Player_Run();
            PlayerSetAnimations();
        }

        if (ControlEnabled)
        {
            Vector3 counterCamera = Vector3.ProjectOnPlane(transform.position - Camera.main.transform.position, Vector3.up).normalized;
            Ray interactionRay = new Ray(transform.position + Vector3.up, counterCamera);
            Debug.DrawRay(interactionRay.origin, interactionRay.origin + counterCamera * interactionRange);
            var rHits = Physics.RaycastAll(interactionRay, interactionRange, interactableLayermask);

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
                if (interactionUI != null)
                    interactionUI.Disable();
            }
        }

        rd.AddForce(Vector3.up * desiredGravityForce, ForceMode.Acceleration);
    }

    public void Attack()
    {
        normalAttackCol.GetComponent<ActiveAttackCol>().LinkDamage = playerAttackDamage; 
        normalAttackCol.SetActive(true); 
        normalAttackCol.GetComponent<ActiveAttackCol>().currentPlayerAction = playerActions[currentActionIndex];

        //SoundModule.Play("Action_Sword");
        OnAttack.Invoke();
    }

    public void AttackAnimExit() 
    {

    }

    public void StepEvent()
    {
        OnStep.Invoke();
    }

    public void AttackAction()
    {
        if (isClicks && !isAttack)
        {

            isAttack = true;
            animator.SetTrigger("Attack1");
            attackParticle.Play();
        }
        
    }

    public void Attack1END()
    {
        if(!isAttack)
        {
            isAttack = false;
            
        }
    }

   /* public void StopAttack()
    {
        isAttack = false;
        for(int i = 0; i < player_Attack_VFX.Length; i++)
        {
            player_Attack_VFX[i].Stop();
        }
    }

    private void AttackAnimationSetup()
    {

    }*/

    public void GetDamage(float damage)
    {
        currentHp -= damage;
    }

    public void FillAmountHP()
    {
        float fillSpeed = 10f;
        playerHp_Bar.fillAmount = Mathf.Lerp(playerHp_Bar.fillAmount, currentHp / maxHp, fillSpeed * Time.deltaTime);
        
    }

    IEnumerator IBlink_HP_Bar()
    {
        while (currentHp < maxHp/2)
        {
            float duration = 1f; // 깜빡임 효과의 전체 지속 시간
            float elapsedTime = 0f;

            Color startColor = new Color(255, 0, 0, 0);
            Color endColor = new Color(255, 0, 0, 1);

            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration) / duration; // 0부터 1까지 보간 비율
                playerHP_Outline.effectColor = Color.Lerp(startColor, endColor, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                float t = Mathf.PingPong(elapsedTime, duration) / duration;

                playerHP_Outline.effectColor = Color.Lerp(endColor, startColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }        
    }



    public void ChangeAction(PlayerAction action)
    {
        action.OnEnterAction(this);

        if (FindObjectOfType(typeof(UI_Tools)))
        {
            UI_Tools tool = (UI_Tools)FindObjectOfType(typeof(UI_Tools));

            tool.SwitchCurrentTool(playerActions.ToArray(), currentActionIndex);
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

    // ???
    private void Move()
    {
        // ??°??? Vector3?? ????
        player_Move_Input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        player_Move_Input.Normalize();

        // ?????? Forward?? ??????
        heading = Camera.main.transform.forward;
        heading.y = 0;
        heading.Normalize();

        heading = heading - player_Move_Input;

        if (player_Move_Input != Vector3.zero && !isAttack)
        {
            isMove = true;

            float angle = Mathf.Atan2(heading.z, heading.x) * Mathf.Rad2Deg * -2;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * rotateSpeed);

            rd.velocity = transform.forward * playerSpeed;

            if (isRun) soundModule.PlayGroup_Run("Player_Run", "Player_Run_1");

            else soundModule.PlayGroup_Walk("Player_Walk", "Player_Walk_1");
        }
        else
        {
            isMove = false;

            soundModule.StopAllCoroutines();
        }
    }

    private void Player_Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isMove)
        {
            playerSpeed = runSpeed;
            isRun = true;
            soundModule.StopAllCoroutines();
        }
        else
        {
            playerSpeed = walkSpeed;
            isRun = false;
            soundModule.StopAllCoroutines();
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

    public void SetValidPlayerActions()
    {
        if (InventoryTitle.instance == null)
        {
            Debug.LogError("InventoryTitle이 없습니다!");
            return;
        }

        foreach (var item in InventoryTitle.instance.InvenItemMapReturn())
        {
            if (item.GetType() == typeof(Equipment_Item))
            {
                Equipment_Item equip = item as Equipment_Item;

                if (item.count >= 1 || equip.belongAlways)
                {
                    if (!playerActions.Contains(equip.AppliedAction))
                        PlayerActions.Add(equip.AppliedAction);
                }
                else
                {
                    if (PlayerActions.Contains(equip.AppliedAction))
                        PlayerActions.Remove(equip.AppliedAction);
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EnemyAttackCol"))
        {
            GetDamage(other.GetComponent<EnemyAttackCol>().Damage);
            amountShake.DORestartById("DamagedPlayer");
        }

        if (other.CompareTag("QuestPos"))
        {
            QuestTitle.instance.QuestPositionCheck(other.name);
        }

    }
    public static void DeActivePlayer()
    {
        PlayerController.Instance.gameObject.SetActive(false);
    }

    public static void SetActivePlayer()
    {
        PlayerController.Instance.gameObject.SetActive(true);
    }

    public void SavePlayerStates()
    {

    }
    public void LoadPlayerStates()
    {

    }
}