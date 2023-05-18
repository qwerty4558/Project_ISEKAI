using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook camFollow;

    public bool isInteraction;
    private void Start()
    {
    }


    private void Update()
    {
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
