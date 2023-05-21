using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private GameObject settingBoard_obj;
    [SerializeField] private GameObject option_obj;
    [SerializeField] public CameraFollow cameraFollow;


    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
             
    }

    private void Start()
    {
        if (cameraFollow == null)
        {
            cameraFollow = FindObjectOfType<CameraFollow>();
        }
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
            }
            else
            {
                settingBoard_obj.SetActive(true);
                if (cameraFollow != null)
                    cameraFollow.isInteraction = true;
            }
        }
    }

    public void OptionActive()
    {
        option_obj.SetActive(true);
    }


}
