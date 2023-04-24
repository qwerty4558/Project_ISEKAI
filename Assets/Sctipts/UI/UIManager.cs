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
    [SerializeField] private bool isSetting;
    [SerializeField] private GameObject settingBoard_obj;
    [SerializeField] private GameObject option_obj;
    [SerializeField] private CameraFollow cameraFollow;


    private void Start()
    {
        isSetting = false;
        
    }

    private void Update()
    {
        //SetActivedUI();
        //CheckScene(SceneManager.GetActiveScene());

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingBoard_obj.activeSelf)
            {
                option_obj.SetActive(false);
                settingBoard_obj.SetActive(false);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                cameraFollow = FindObjectOfType<CameraFollow>();
                settingBoard_obj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void OptionActive()
    {
        option_obj.SetActive(true);
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
