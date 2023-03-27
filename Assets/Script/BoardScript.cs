using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Board;//ui�ǳ�
    public GameObject Boardpick;//����Ʈ���� ���� ��
    public GameObject ItemManager; // ������ �Ŵ���

    private bool state;//�ǳڻ���
    private bool BoardPick;

    void Start()
    {
        Combination_Item combinationItem = ItemManager.GetComponent<Combination_Item>();

        combinationItem.PrintResultItemInfo();
        /* combinationItem.PrintRandomResultItemInfo(); */

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
            if (Input.GetKeyDown(KeyCode.T)) state = !state;
            Debug.Log(BoardPick);
            //Debug.Log(state);
        }
        if (state)
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