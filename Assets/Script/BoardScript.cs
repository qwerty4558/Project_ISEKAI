using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Board;//ui판넬
    public GameObject Boardpick;//퀘스트보드 접근 시
    private bool state;//판넬상태
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

        //if (BoardPick == true)
        //{
        //    if (Input.GetKeyDown(KeyCode.T))
        //    {
        //        state = !state;

        //    }
        //}
        //ViewUI();
        //if (BoardPick == true)
        //{
        /*if (Input.GetKeyDown(KeyCode.T)) state = !state;
        ViewUI();*/
        //}

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

    private void OnTriggerStay(Collider col)///이부분에서 콜라이더 비교~~~~~~~
    {
        if (col.CompareTag("Player"))
        {
            BoardPick = false;
            Debug.Log("Un Taged player  " + BoardPick);
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