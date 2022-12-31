using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] Text pickUpText;
    bool isPickUp;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
        
    }

    private void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

}
