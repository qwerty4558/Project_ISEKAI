using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public string equipment_Name; // ��� �̸�

    public float range; // ����� ��Ÿ�
    public int damage; // ����� �����

    public float work_Speed; // �۾� �ӵ�
    public float work_Time; // �۾� �ð�
    public float work_Active_Delay; // �۾� Ȱ��ȭ 
    public float work_Disable_Delay; // �۾� ��Ȱ��ȭ

    public Animator anim;
}
