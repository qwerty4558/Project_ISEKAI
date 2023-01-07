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

    Rigidbody rigidbody;
    Animator animator;


    /*[SerializeField] float dashSpeed = 7f;*/


    Vector3 dir = Vector3.zero;

    Vector3 heading;

    bool isMove = false;



    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

   
    void FixedUpdate()
    {
        Move();
        Player_Run();
        PlayerSetAnimations();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree")) Debug.Log("Enter Tree");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                other.GetComponent<Tree>().Mine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree")) Debug.Log("Exit Tree");
    }

    private void Move()
    {

        hor_Move = Input.GetAxis("Horizontal");
        ver_Move = Input.GetAxis("Vertical");

        heading = Camera.main.transform.rotation * Vector3.forward;
        heading.y = 0;
        heading = heading.normalized;

        dir = heading * Time.deltaTime * ver_Move * moveSpeed;
        dir += Quaternion.Euler(0, 90, 0) * heading * Time.deltaTime * hor_Move * moveSpeed;

        if (dir != Vector3.zero)
        {
            isMove = true;
            //transform.forward = Vector3.Lerp(transform.forward, dir, rotateSpeed * Time.deltaTime);            
        }
        else
        {
            isMove = false;            
        }
        //rigidbody.MovePosition(dir  * Time.deltaTime);
        transform.Translate(dir);
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
