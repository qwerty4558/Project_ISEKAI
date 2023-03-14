using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField] float range;

    private bool pickupActived = false;

    private Collider col;
    private RaycastHit hitInfo;
    [SerializeField] private TMP_Text actionText;

    private void Start()
    {
        pickupActived = false;
        actionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if (pickupActived)
            {
                CanPickUp(col);
            }
            else return;
        }
}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemInfoAppare(other);
            col = other;
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

    private void ItemInfoAppare(Collider col)
    {
        pickupActived = true ;
        actionText.gameObject.SetActive(true);
        actionText.text = col.transform.GetComponent<ItemPickUp>().item.name_KR + " ȹ��!" + " (F)" ;
    }

    private void CanPickUp(Collider col)
    {
        if (pickupActived == true)
        {
            Debug.Log(col.transform.GetComponent<ItemPickUp>().item.name_KR + "ȹ���߽��ϴ�."); // �� �κ��� �κ��丮�� ����(����  �۾�)
            
            Destroy(col.transform.gameObject);
            ItemInfoDisAppare();            
        }
    }
}
