using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss_ItemObject : MonoBehaviour
{
    PlayerController playerController;

    [SerializeField,Required] Ingredient_Item itemData;

    [SerializeField] private float detectionRange = 1.0f;
    [SerializeField] private float hitRange = 0.1f;
    [SerializeField] private bool isGet;
    [SerializeField] float itemSpeed = 0.25f;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(ItemDrop());
    }

    private IEnumerator ItemDrop()
    {
        yield return new WaitForSeconds(1f);
        isGet = true;
    }


    private void FixedUpdate()
    {
        if(playerController != null)
        {
            if(Vector3.Distance(playerController.transform.position, transform.position) < detectionRange && isGet)
            {
                Vector3 dir = playerController.transform.position - transform.position;
                transform.Translate(dir.normalized * Mathf.Clamp(dir.magnitude, 0, itemSpeed));
            }
            if(Vector3.Distance(playerController.transform.position, transform.position) < hitRange && isGet)
            {
                GetItem();
                Destroy(gameObject);
            }            
        }
    }

    public void GetItem()
    {
        ItemInfoUI itemInfoUI = FindObjectOfType<ItemInfoUI>();
        if (itemInfoUI != null)
            itemInfoUI.InsertSlot(itemData);


        InventoryTitle inven = FindObjectOfType<InventoryTitle>();
        if (inven != null)
            /*inven.PlusItem(itemData);*/
            inven.AlchemyItemPlus(itemData);
    }
}
