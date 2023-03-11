using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Board;
    public GameObject Boardpick;
    private bool state;
    bool BoardPick = false;

    void Start()
    {
        Boardpick.SetActive(false);
        state = false;
        Board.SetActive(state);
    }

    void Update()
    {
        Debug.Log(state);
        if (Input.GetKeyDown(KeyCode.T)) state = !state;
        ViewUI();

    }

    void ViewUI()
    {
        Board.SetActive(state);
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
        }
    }

}
