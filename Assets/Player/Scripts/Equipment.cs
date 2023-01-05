using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public string equipment_Name; // 장비 이름

    public float range; // 장비의 사거리
    public int damage; // 장비의 대미지

    public float work_Speed; // 작업 속도
    public float work_Time; // 작업 시간
    public float work_Active_Delay; // 작업 활성화 
    public float work_Disable_Delay; // 작업 비활성화

    public Animator anim;
}
