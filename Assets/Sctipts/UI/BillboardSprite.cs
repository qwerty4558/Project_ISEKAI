using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main == null) return;

        transform.forward = Camera.main.transform.position - transform.position;
    }
}
