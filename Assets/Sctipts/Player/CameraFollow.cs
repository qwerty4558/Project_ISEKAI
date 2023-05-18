using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public CinemachineFreeLook camFollow;
    [SerializeField] string nowSceneName;
    public bool isInteraction;
    private void Start()
    {
    }


    private void Update()
    {
        nowSceneName = SceneManager.GetActiveScene().name;

        

        if (camFollow == null) return;

        if (!isInteraction)
        {

            camFollow.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            camFollow.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }

    private void LateUpdate()
    {
       
    }
}
