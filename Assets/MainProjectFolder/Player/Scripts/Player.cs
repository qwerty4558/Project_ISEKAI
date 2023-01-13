using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어의 입력 값 저장
    float move_hor;
    float move_ver;

    [Header("플레이어의 이동 관련 수치")]
    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _rotateSpeed = 30f;

    [SerializeField] Canvas canvas;

    Rigidbody rig;
    Animator animator;


    
}
