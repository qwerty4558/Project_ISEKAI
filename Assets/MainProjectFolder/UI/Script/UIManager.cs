using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum WATCH_PAGE
{
    MAIN_PAGE,
    EQUIPMENT_PAGE,
    LEARNING_PAGE,
    INVENTORY
}


public class UIManager : MonoBehaviour
{
    public Transform gameUI;
    [Header("Children Page")]
    [SerializeField] GameObject watch_Main_Page;
    [SerializeField] GameObject watch_Equipment_Page;
    [SerializeField] GameObject watch_Learning_Page;
    [SerializeField] GameObject inventory_Popup;

    
    [SerializeField] bool is_now_inventory;
    
    
    [SerializeField]bool isUIActive = false;
    WATCH_PAGE now_Page;
    

    // Start is called before the first frame update
    void Start()
    {
        now_Page = WATCH_PAGE.MAIN_PAGE;
        watch_Main_Page.transform.DOLocalMoveX(388, 1);
        watch_Equipment_Page.transform.DOLocalMoveX(-400, 1);
        watch_Learning_Page.transform.DOLocalMoveX(-400, 1);
    }

    // Update is called once per frame
    void Update()
    {
        NowPageSetting();
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isUIActive)
            {
                gameUI.DOMoveX(620, 0.5f);
                isUIActive = true;
            }
            else
            {
                gameUI.DOMoveX(-420, 0.5f);
                isUIActive = false;
                inventory_Popup.SetActive(false);
            }
        }
    }

    public void ClickToPage(int to_page)
    {
        now_Page = (WATCH_PAGE)to_page;
        switch (now_Page)
        {
            case WATCH_PAGE.MAIN_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(388, 0.5f);
                watch_Equipment_Page.transform.DOLocalMoveX(-400, 0.5f);
                watch_Learning_Page.transform.DOLocalMoveX(-400, 0.5f);
                inventory_Popup.SetActive(false);
                break;
            case WATCH_PAGE.EQUIPMENT_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(-400, 0.5f);
                watch_Equipment_Page.transform.DOLocalMoveX(388, 0.5f);
                watch_Learning_Page.transform.DOLocalMoveX(-400, 0.5f);
                inventory_Popup.SetActive(false);
                break;
            case WATCH_PAGE.LEARNING_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(-400, 0.5f);
                watch_Equipment_Page.transform.DOLocalMoveX(-400, 0.5f);
                watch_Learning_Page.transform.DOLocalMoveX(388, 0.5f);
                inventory_Popup.SetActive(false);
                break;
            case WATCH_PAGE.INVENTORY:
                inventory_Popup.SetActive(true);
                break;
        }
    }
    
    public void NowPageSetting()
    {
        
    }

}
