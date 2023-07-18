using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WItchs_Bullet : MonoBehaviour
{
    [SerializeField] EnemyAttackCol attackCol;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            Destroy(this.gameObject);
        }
    }
}
