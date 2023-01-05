using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotateSpeed = 30f;

    Rigidbody rigidbody;
    Animator animator;
    /*[SerializeField] float dashSpeed = 7f;*/


    Vector3 dir = Vector3.zero;
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
        
    }

    private void Move()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        if (dir != Vector3.zero)
        {
            isMove = true;
            transform.forward = Vector3.Lerp(transform.forward, dir/2, rotateSpeed * Time.deltaTime);
            animator.SetBool("isWalk", isMove);
        }
        else
        {
            isMove = false;
            animator.SetBool("isWalk", isMove);
        }

        rigidbody.MovePosition(this.gameObject.transform.position + dir * moveSpeed * Time.deltaTime);
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
}
