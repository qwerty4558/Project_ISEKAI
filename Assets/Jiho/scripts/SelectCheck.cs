using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCheck : MonoBehaviour
{
    [SerializeField] private Image selectImage;
    [SerializeField] private Image clearImage;

    public void SelectItem(Image image)
    {
        selectImage.sprite = image.sprite;
        clearImage.sprite = image.sprite;
        selectImage.enabled = true;
    }
}
