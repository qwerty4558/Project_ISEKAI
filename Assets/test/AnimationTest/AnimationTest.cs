using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    private Animator animator;
    bool isAttack_01, isAttack_02, isAttack_03;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isAttack_01 = false;
        isAttack_02 = false;
        isAttack_03 = false;
    }

    private void Update()
    {
        GetKeyCode();
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        DoAnimation();

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_01") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            Debug.Log("Exit Attack 01");
            isAttack_01 = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_02") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            Debug.Log("Exit Attack 02");
            isAttack_02 = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_03") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            Debug.Log("Exit Attack 03");
            isAttack_03 = false;
        }
    }

    private void GetKeyCode()
    {
        if (isAttack_01 == false && isAttack_02 == false && isAttack_03 == false)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                isAttack_01 = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                isAttack_02 = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                isAttack_03 = true;
            }
        }
    }

    void DoAnimation()
    {
        animator.SetBool("isAttack01", isAttack_01);
        animator.SetBool("isAttack02", isAttack_02);
        animator.SetBool("isAttack03", isAttack_03);
    }
}
