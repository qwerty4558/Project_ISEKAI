using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trigger : MonoBehaviour
{
    public string ChatText = "";
    private GameObject Main;
    private GameObject Player;

    void Start()
    {
        Debug.Log("Object found");
        Main = GameObject.Find("Main");
        Player = GameObject.Find("Cube");
    }

    void Update()
    {
        if (Main.GetComponent<MainScript>().NPCButton.activeSelf == true && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Dialog On");
            Player.GetComponent<MoveController>().enabled = false;

            Main.GetComponent<MainScript>().NPCChatButtonNoShow();
            Main.GetComponent<MainScript>().NPCChatEnter(ChatText);
        }

        if (Main.GetComponent<MainScript>().NPCButton.activeSelf == false && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Dialog Off");
            Player.GetComponent<MoveController>().enabled = true;

            Main.GetComponent<MainScript>().NPCChatButtonShow();
            Main.GetComponent<MainScript>().NPCChatExit();
        }
    }
    private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "myplayer")
            {
                Debug.Log("Trigger On");
                Main.GetComponent<MainScript>().NPCChatButtonShow();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "myplayer")
            {
                Debug.Log("Trigger Off");
                Main.GetComponent<MainScript>().NPCChatButtonNoShow();
            }
        }
    }

