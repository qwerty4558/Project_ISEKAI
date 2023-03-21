using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionController : MonoBehaviour
{

    private bool pickupActived = false;


    [SerializeField] private TMP_Text actionText;

    private void Start()
    {
        pickupActived = false;
        actionText.gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            CanPickUp(other);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemInfoDisAppare();
        }
    }

    private void ItemInfoDisAppare()
    {
        pickupActived = false ;
        actionText.gameObject.SetActive(false);
    }



    private void CanPickUp(Collider col)
    {  
            Debug.Log(col.transform.GetComponent<ItemPickUp>().item.name_KR + "ȹ���߽��ϴ�."); // �� �κ��� �κ��丮�� ����(����  �۾�)
            Destroy(col.transform.gameObject);
            ItemInfoDisAppare();            
    }
}
