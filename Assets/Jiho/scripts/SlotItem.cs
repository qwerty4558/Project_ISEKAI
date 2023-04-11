using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private int id;
    [SerializeField] private int count;
    [SerializeField] private Image slotImage;
    [SerializeField] private string status;
    [SerializeField] private string route;

    private Vector3 vec;
    private Vector3 popStartPos;
    private Vector3 popEndPos;
    private bool isCheck;
    private bool isPickUp;
    private bool timeCheck;
    private bool isPop;
    private float time;
    private float popTimer;


    public TextMeshProUGUI[] statusTexts;
    public GameObject itemStatus;
    public Sprite itemImage;
    public GameObject playerPrefab;

    public string ItemName { get => itemName; set => itemName = value;  }
    public string Status { get => status; set => status = value; }
    public string Route { get => route; set => route = value; }
    public int ID { get => id; set => id = value; }
    public int Count { get => count; set => count = value;  }
    public bool IsCheck { get => isCheck; set => isCheck = value; }
    public bool IsPickUp { get => isPickUp; set => isPickUp = value; }
    public bool IsPop { get => isPop; }

    private void Awake()
    {
        isPop = true;
        time = 0.5f;
        PopItem();
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private void PopItem()
    {
        float random = UnityEngine.Random.Range(transform.position.x - (transform.localScale.x * 5), transform.position.x + (transform.localScale.x * 5));
        float random2 = UnityEngine.Random.Range(transform.position.z - (transform.localScale.z * 5), transform.position.z + (transform.localScale.z * 5));

        popStartPos = transform.position;
        popEndPos = new Vector3(random, transform.position.y, random2);
        StartCoroutine(PopCor());
    }

    private IEnumerator PopCor()
    {
        popTimer = 0;
        while(transform.position.y >= popStartPos.y)
        {
            popTimer += Time.deltaTime;
            Vector3 tempPos = Parabola(popStartPos, popEndPos, 5, popTimer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }
        isPop = false;
    }

    private void Update()
    {
        if(!isCheck && this.gameObject.CompareTag("SlotItem"))
        {
            if(id != 0)
            {
                isCheck = true;
                slotImage.enabled = true;
                slotImage.sprite = itemImage;
            }
            else
            {
                isCheck = false;
                slotImage.enabled = false;
            }
        }

        if(this.gameObject.CompareTag("FieldItem") && !isPickUp && !isPop)
        {
            if (time >= 0.3f && !timeCheck)
                time -= Time.deltaTime * 0.5f;
            else if (time <= 0.5f && timeCheck)
                time += Time.deltaTime * 0.5f;
            else
                timeCheck = !timeCheck;

            transform.position = new Vector3(transform.position.x, time, transform.position.z);
        }

        if(this.gameObject.CompareTag("FieldItem") && Vector3.Distance(playerPrefab.transform.position, transform.position) < 2 && !isPop)
        {
            isPickUp = true;
            vec = playerPrefab.transform.position - transform.position;
            transform.position = transform.position + vec * Time.deltaTime * 3;
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.CompareTag("SlotItem"))
        {
            statusTexts[0].text = itemName;
            statusTexts[1].text = status;
            statusTexts[2].text = route;

            itemStatus.transform.position = this.transform.position;
            itemStatus.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.gameObject.CompareTag("SlotItem"))
            itemStatus.SetActive(false);
    }
}
