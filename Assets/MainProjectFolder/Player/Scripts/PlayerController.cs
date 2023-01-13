using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerController : MonoBehaviour
{

    float hor_Move;
    float ver_Move;

    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 30f;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject recipi;
    [SerializeField] GameObject map;
    Rigidbody rigidbody;
    Animator animator;


    //[SerializeField] float dashSpeed = 7f;


    Vector3 dir = Vector3.zero;

    Vector3 heading;

    bool isMove = false;
    bool isInventoryActive = false;
    bool isRecipiActive = false;
    bool isMapActive = false;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inventory.SetActive(isInventoryActive);
        recipi.SetActive(isRecipiActive);
        map.SetActive(isMapActive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) isInventoryActive = !isInventoryActive;
        if (Input.GetKeyDown(KeyCode.R)) isRecipiActive = !isRecipiActive;
        if (Input.GetKeyDown(KeyCode.M)) isMapActive = !isMapActive;
        ViewInventory(isInventoryActive);
        ViewRecipi(isRecipiActive);
        ViewMap(isMapActive);
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

    private void ViewInventory(bool isActive)
    {
        inventory.SetActive(isActive);
    }

    private void ViewRecipi(bool isActive)
    {
        recipi.SetActive(isActive);
    }
    private void ViewMap(bool isActive)
    {
        map.SetActive(isActive);
    }

    private void Move()
    {
        hor_Move = Input.GetAxis("Horizontal");
        ver_Move = Input.GetAxis("Vertical");

        heading = Camera.main.transform.forward;
        heading.y = 0;
        heading = heading.normalized;

        

        dir = heading * Time.deltaTime * ver_Move * moveSpeed;
        dir += Quaternion.Euler(0, 90, 0) * heading * Time.deltaTime * hor_Move * moveSpeed;

        if (dir != Vector3.zero)
        {
            isMove = true;
            transform.Translate(dir);
            //transform.forward = Vector3.Lerp(transform.forward, dir, rotateSpeed * Time.deltaTime);            
            // 
        }
        else
        {
            isMove = false;
        }
        //rigidbody.MovePosition(dir  * Time.deltaTime);


    }

    private void Player_Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isMove)
        {
            moveSpeed = 5.5f;
            animator.SetBool("isJog", true);
        }
        else
        {
            moveSpeed = 4f;
            animator.SetBool("isJog", false);
        }
    }
    private void PlayerSetAnimations()
    {
        animator.SetBool("isWalk", isMove);
    }
}
