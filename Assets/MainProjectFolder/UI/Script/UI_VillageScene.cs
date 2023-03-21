using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum WATCH_PAGE
{
    MAIN_PAGE,
    INVENTORY_PAGE,
    MAP_PAGE,
    RECIPE_PAGE
}

public class UI_VillageScene : MonoBehaviour
{
    public Transform gameUI;
    [Header("Children Page")]
    [SerializeField] GameObject watch_Main_Page;
    [SerializeField] GameObject map_Page;
    [SerializeField] GameObject inventory_Page;
    [SerializeField] GameObject recipe_Page;


    [SerializeField] bool isUIActive = false;
    WATCH_PAGE now_Page;


    // Start is called before the first frame update
    void Start()
    {
        now_Page = WATCH_PAGE.MAIN_PAGE;
        watch_Main_Page.transform.DOLocalMoveX(388, 1);
        inventory_Page.transform.DOLocalMoveX(-400, 1);
        map_Page.transform.DOLocalMoveX(-400, 1);
        recipe_Page.transform.DOLocalMoveX(-400, 1);
    }

    // Update is called once per frame
    void Update()
    {
        ShowWatch();
        
    }

    private void ShowWatch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isUIActive)
            {
                gameUI.DOLocalMoveX(120, 0.5f);
                isUIActive = true;
                Cursor.lockState = CursorLockMode.None ;
            }
            else
            {
                gameUI.DOLocalMoveX(-840, 0.5f);
                isUIActive = false;
            }
        }
        Cursor.visible = isUIActive;
    }

    public void ClickToPage(int to_page)
    {
        now_Page = (WATCH_PAGE)to_page;
        watch_Main_Page.transform.DOLocalMoveX(-400, 1);
        inventory_Page.transform.DOLocalMoveX(-400, 1);
        map_Page.transform.DOLocalMoveX(-400, 1);
        recipe_Page.transform.DOLocalMoveX(-400, 1);
        switch (now_Page)
        {
            case WATCH_PAGE.MAIN_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(388, 1);
                break;
            case WATCH_PAGE.INVENTORY_PAGE:                
                inventory_Page.transform.DOLocalMoveX(388, 1);
                break;
            case WATCH_PAGE.MAP_PAGE:
                map_Page.transform.DOLocalMoveX(388, 1);
                break;
            case WATCH_PAGE.RECIPE_PAGE:
                recipe_Page.transform.DOLocalMoveX(388, 1);
                break;
        }
    }
}
