using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾��� �Է� �� ����
    float move_hor;
    float move_ver;

    [Header("�÷��̾��� �̵� ���� ��ġ")]
    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _rotateSpeed = 30f;

    [SerializeField] Canvas canvas;

    Rigidbody rig;
    Animator animator;


    
}
