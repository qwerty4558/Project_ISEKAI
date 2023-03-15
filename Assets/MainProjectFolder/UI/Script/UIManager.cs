using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : Managers
{
    [SerializeField] GameObject mainScene_UI;
    [SerializeField] GameObject gamePlayInVillageScene;
    [SerializeField] GameObject shopScene;
    [SerializeField] bool isTitle;
    [SerializeField] bool isPlayInVillage;
    [SerializeField] bool isShop;


    private void Start()
    {
        
    }

    private void Update()
    {
        SetActivedUI();
        CheckScene(SceneManager.GetActiveScene());
    }

    public void CheckScene(Scene s)
    {
        isTitle = false; 
        isShop = false;
        isPlayInVillage= false;

        switch (s.name)
        {
            case "Title":
                isTitle = true;
                break;
            case "L_Main":
                isPlayInVillage= true;
                break;
            case "L_forest":
                isPlayInVillage = true;
                break;
            case "L_mine":
                isPlayInVillage = true;
                break;
            case "Shop":
                isShop= true;
                break;
        }
        
    }

    private void SetActivedUI()
    {
        mainScene_UI.SetActive(isTitle);
        gamePlayInVillageScene.SetActive(isPlayInVillage);
        shopScene.SetActive(isShop);
    }
}
