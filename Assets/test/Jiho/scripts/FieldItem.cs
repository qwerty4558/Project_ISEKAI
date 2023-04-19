using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldItem : MonoBehaviour
{
    //private float popTimer;
    //private float idleTimer;
    //private bool isIdleTimeCheck;
    //private bool isPop;
    //private bool isPickUp;
    //private Vector3 vec;
    //private Vector3 popStartPos;
    //private Vector3 popEndPos;

    //private GameObject player_obj;

    ////아이템 정보
    //public Item itemData;
    //public Sprite itemImage;
    //public string itemName;
    //public string itemStatus;
    //public string itemRoute;
    //public int itemCount;

    //public GameObject Player_obj { get => player_obj; set => player_obj = value; }
    //public float PopTimer { get => popTimer; set => popTimer = value; }
    //public bool IsPop { get => isPop; set => isPop = value; }



    //private void Awake()
    //{
    //    itemData = new Item(itemImage, itemName, itemStatus, itemRoute, itemCount);
    //    isPop = true;
    //    PopItem();
    //}

    //private void Update()
    //{
    //    IdleItem();
    //    MoveItem();
    //}

    //private void PopItem()
    //{
    //    float random = UnityEngine.Random.Range(transform.position.x - (transform.localScale.x * 5), transform.position.x + (transform.localScale.x * 5));
    //    float random2 = UnityEngine.Random.Range(transform.position.z - (transform.localScale.z * 5), transform.position.z + (transform.localScale.z * 5));

    //    popStartPos = transform.position;
    //    popEndPos = new Vector3(random, transform.position.y, random2);
    //    StartCoroutine(PopCor());
    //}

    //private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    //{
    //    Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

    //    var mid = Vector3.Lerp(start, end, t);

    //    return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    //}

    //private IEnumerator PopCor()
    //{
    //    popTimer = 0;
    //    while (transform.position.y >= popStartPos.y)
    //    {
    //        popTimer += Time.deltaTime;
    //        Vector3 tempPos = Parabola(popStartPos, popEndPos, 5, popTimer);
    //        transform.position = tempPos;
    //        yield return new WaitForEndOfFrame();
    //    }
    //    isPop = false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        PlayerController player = other.GetComponent<PlayerController>();
    //        player.inven.PlusItem(this.itemData);
    //        Destroy(this.gameObject);
    //    }
    //}

    //private void IdleItem()
    //{
    //    if(!isPop && !isPickUp)
    //    {
    //        if (idleTimer >= 0.3f && !isIdleTimeCheck)
    //            idleTimer -= Time.deltaTime * 0.5f;
    //        else if (idleTimer <= 0.5f && isIdleTimeCheck)
    //            idleTimer += Time.deltaTime * 0.5f;
    //        else
    //            isIdleTimeCheck = !isIdleTimeCheck;

    //        transform.position = new Vector3(transform.position.x, idleTimer, transform.position.z);
    //    }
    //}

    //private void MoveItem()
    //{
    //    if(Vector3.Distance(player_obj.transform.position, transform.position) < 2 && !isPop)
    //    {
    //        isPickUp = true;
    //        vec = player_obj.transform.position - transform.position;
    //        transform.position = transform.position + vec * Time.deltaTime * 5;
    //    }
    //}
}
