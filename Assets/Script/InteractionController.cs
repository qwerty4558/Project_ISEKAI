//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InteractionController : MonoBehaviour
//{

//    bool isContact;
//    [SerializeField] GameObject go_InteractChecked;

//    void Update()
//    {
//        bool isContact = false;
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        print(other.gameObject.tag);
//        if (other.gameObject.tag == "NPC")
//        {
//            isContact = true;
//            Debug.Log("Contacted");
//            GameObject panel = go_InteractChecked.gameObject;
//            if (panel == null)
//            {
//                return;
//            }
//            panel.SetActive(true);
//        }
//    }


//    void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.tag == "NPC")
//        {
//            isContact = false;
//            Debug.Log("Not Contacted");

//            go_InteractChecked.SetActive(false);
//        }
//    }

//    void OnTriggerStay(Collider other)
//    {
//        if (Input.GetKeyDown(KeyCode.F))
//        {
//            if (other.gameObject.tag == "NPC")
//            {
//                go_InteractChecked.SetActive(false);
//                theDM.ShowDialogue(other.GetComponent<InteractionEvent>().GetDialogue());
//            }

//        }
//    }

//}
