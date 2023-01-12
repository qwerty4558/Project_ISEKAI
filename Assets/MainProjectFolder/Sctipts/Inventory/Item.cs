using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    SphereCollider col;
    private void Start()
    {
        col = GetComponent<SphereCollider>();
    }

}
