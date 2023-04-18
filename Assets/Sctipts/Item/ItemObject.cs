using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField,Required] Ingredient_Item itemData;

    [SerializeField] private float detectionRange = 1.0f;
    [SerializeField] private float hitRange = 0.1f;

    [SerializeField] float itemSpeed = 0.25f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        if(playerController != null)
        {
            if(Vector3.Distance(playerController.transform.position, transform.position) < detectionRange)
            {
                Vector3 dir = playerController.transform.position - transform.position;
                transform.Translate(dir.normalized * Mathf.Clamp(dir.magnitude, 0, itemSpeed));
            }
            if(Vector3.Distance(playerController.transform.position, transform.position) < hitRange)
            {
                GetItem();
                Destroy(gameObject);
            }            
        }
    }

    public void GetItem()
    {
        if(ItemInfoUI.Instance != null)
        {
            ItemInfoUI.Instance.InsertSlot(itemData);
        }
    }


}
