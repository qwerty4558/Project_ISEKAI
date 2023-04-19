using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI_Single : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image itemImage;

    public void SetUIInfo(string name, Sprite image)
    {
        text.text = name;
        itemImage.sprite = image;
    }

    private void OnEnable()
    {
        Invoke("DestroyThis",10f);
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

}
