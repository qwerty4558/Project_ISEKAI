using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour ,IDamage
{

    [SerializeField] float playerHp = 100f;

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 40f;
    [SerializeField] float interactionRange = 2f;
    [SerializeField] float playerAttackDamage = 1f;

    [SerializeField] GameObject inventory;
    [SerializeField] GameObject recipi;
    [SerializeField] GameObject map;

    Rigidbody rigidbody;
    Animator animator;

    BoxCollider hitCollider;

    //[SerializeField] float dashSpeed = 7f;

    Vector3 player_Move_Input;

    Vector3 heading;

    bool isMove = false;
    bool isRun = false;
    bool isInventoryActive = false;
    bool isRecipiActive = false;
    bool isMapActive = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        hitCollider = GetComponent<BoxCollider>();

        inventory.SetActive(isInventoryActive);
        recipi.SetActive(isRecipiActive);
        map.SetActive(isMapActive);
    }

    private void Update()
    {
        InputCheckBool();
        ViewUI();
    }

    void FixedUpdate()
    {
        if (!isMapActive)
        {
            Move();
            Player_Run();
            PlayerSetAnimations();
        }
        else return;

    }

    private void ViewUI()
    {
        inventory.SetActive(isInventoryActive);
        recipi.SetActive(isRecipiActive);
        map.SetActive(isMapActive);

        if (Input.GetMouseButtonDown(0)) Interaction();
    }

    private void InputCheckBool()
    {
        if (Input.GetKeyDown(KeyCode.I)) isInventoryActive = !isInventoryActive;
        if (Input.GetKeyDown(KeyCode.R)) isRecipiActive = !isRecipiActive;
        if (Input.GetKeyDown(KeyCode.M)) isMapActive = !isMapActive;
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
            IDamage damage = hit.collider.GetComponent<IDamage>();
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

        //Debug.Log("input Vector : " + player_Move_Input);
        //Debug.Log("this rotation y value : " + transform.localRotation.eulerAngles.y)
                
        // 카메라의 Forward를 가져옴
        heading = Camera.main.transform.forward;
        heading.y = 0;
        heading.Normalize();

        heading = heading - player_Move_Input;

        if (player_Move_Input != Vector3.zero)
        {
            isMove = true;

            float angle = Mathf.Atan2(heading.z, heading.x) * Mathf.Rad2Deg * -2;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, angle, 0), Time.deltaTime * rotateSpeed);

            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
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
            moveSpeed = 5.5f;
            isRun = true;
        }
        else
        {
            moveSpeed = 4f;
            isRun = false;
        }
    }
    private void PlayerSetAnimations()
    {
        animator.SetBool("isWalk", isMove);
        animator.SetBool("isJog", isRun);
    }

    public void Damage(float damage)
    {
        
    }
}
