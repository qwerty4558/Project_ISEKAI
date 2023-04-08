using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCheck : MonoBehaviour
{
    [SerializeField] private Image selectImage;

    public void SelectItem(Image image)
    {
        selectImage.sprite = image.sprite;
        selectImage.enabled = true;
    }
}
