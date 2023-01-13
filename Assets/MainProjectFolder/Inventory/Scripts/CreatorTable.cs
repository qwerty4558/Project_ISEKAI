using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorTable : MonoBehaviour
{
   // [SerializeField] GameObject emojiUI;
    [SerializeField] GameObject inventory;
    SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider= GetComponent<SphereCollider>();        
       // emojiUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tag" + other.tag);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            // emojiUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                inventory.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       // emojiUI.SetActive(false);
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
