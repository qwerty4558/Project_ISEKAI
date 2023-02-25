using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

enum WATCH_PAGE
{
    MAIN_PAGE,
    INVENTORY_PAGE,
    MAP_PAGE,
    RECIPE_PAGE
}


public class UIManager : MonoBehaviour
{
    public Transform gameUI;
    [Header("Children Page")]
    [SerializeField] GameObject watch_Main_Page;
    [SerializeField] GameObject map_Page;
    [SerializeField] GameObject inventory_Page;
    [SerializeField] GameObject recipe_Page;
    
    
    [SerializeField]bool isUIActive = false;
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
       
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isUIActive)
            {
                gameUI.DOLocalMoveX(120, 0.5f);
                isUIActive = true;
            }
            else
            {
                gameUI.DOLocalMoveX(-840, 0.5f);
                isUIActive = false;
                
            }
        }
    }

    public void ClickToPage(int to_page)
    {
        now_Page = (WATCH_PAGE)to_page;

        switch (now_Page)
        {
            case WATCH_PAGE.MAIN_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(388, 1);
                inventory_Page.transform.DOLocalMoveX(-400, 1);
                map_Page.transform.DOLocalMoveX(-400, 1);
                recipe_Page.transform.DOLocalMoveX(-400, 1);
                break;
            case WATCH_PAGE.INVENTORY_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(-400, 1);
                inventory_Page.transform.DOLocalMoveX(388, 1);
                map_Page.transform.DOLocalMoveX(-400, 1);
                recipe_Page.transform.DOLocalMoveX(-400, 1);
                break;
            case WATCH_PAGE.MAP_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(-400, 1);
                inventory_Page.transform.DOLocalMoveX(-400, 1);
                map_Page.transform.DOLocalMoveX(388, 1);
                recipe_Page.transform.DOLocalMoveX(-400, 1);
                break;
            case WATCH_PAGE.RECIPE_PAGE:
                watch_Main_Page.transform.DOLocalMoveX(-400, 1);
                inventory_Page.transform.DOLocalMoveX(-400, 1);
                map_Page.transform.DOLocalMoveX(-400, 1);
                recipe_Page.transform.DOLocalMoveX(388, 1);
                break;
        }
       
    }
}
