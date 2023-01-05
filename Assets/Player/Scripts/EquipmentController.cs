using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentController : MonoBehaviour
{
    [SerializeField] protected Equipment current_Equipment;
    protected bool isSwing = false;
    protected bool isHit = false;

    protected RaycastHit htiinfo;

    protected void TryHit()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(!isHit)
            {
                StartCoroutine(HitCorutine());
            }
        }
    }

    protected IEnumerator HitCorutine()
    {
        isHit = true;
        current_Equipment.anim.SetTrigger("Hit");

        yield return new WaitForSeconds(current_Equipment.work_Active_Delay);
        isSwing = true;



        yield return new WaitForSeconds(current_Equipment.work_Disable_Delay);
        isSwing = false;

        yield return new WaitForSeconds(current_Equipment.work_Time - current_Equipment.work_Active_Delay - current_Equipment.work_Disable_Delay);
        isHit = false;

    }
}
