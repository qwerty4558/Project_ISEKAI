using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Board;//uiÆÇ³Ú
    public GameObject Boardpick;//Äù½ºÆ®º¸µå Á¢±Ù ½Ã
    private bool state;//ÆÇ³Ú»óÅÂ
    private bool BoardPick;

    void Start()
    {
        Boardpick.SetActive(false);
        state = false;
        BoardPick = false;
        Board.SetActive(state);
    }

    void Update()
    {
        Debug.Log(state);

        if (BoardPick)
        {
            if(Input.GetKeyDown(KeyCode.T)) state = !state;
            Debug.Log(BoardPick);
            //Debug.Log(state);
        }
        if(state)
        {
            BoardPick = false;
        }
        ViewUI();
    }

    void ViewUI()
    {
        Board.SetActive(state);
        Boardpick.SetActive(BoardPick);
    }

    public void ClickNoButton()
    {
        state = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            BoardPick = true;
            Debug.Log("Taged player  " + BoardPick);

        }

    }


    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            BoardPick = false;
            Debug.Log("Un Taged player  " + BoardPick);
            state = false;
        }
    }

}