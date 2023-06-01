using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook camFollow;
    
    public bool isInteraction;
    private void Start()
    {
        isInteraction = false;
        CursorManage.instance.HideMouse();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        isInteraction = false;
        CursorManage.instance.HideMouse();
        Vector3 pPos = PlayerController.instance.transform.position;
        Vector3 newVec = pPos + new Vector3(0, 5, -10);
        Quaternion newRot = Quaternion.LookRotation(pPos - newVec, Vector3.up);
        SetCameraPosAndRotation(newVec, newRot);
    }


    public void SetCameraPosAndRotation(Vector3 p, Quaternion r)
    {
        if(camFollow != null)
        {
            camFollow.transform.position = p;
            camFollow.transform.rotation = r;
            camFollow.Follow = PlayerController.instance.transform;
        }
    }

    private void Update()
    {
        if (camFollow == null) return;
        if (!isInteraction)
        {
            camFollow.enabled = true;
            CursorManage.instance.HideMouse();
        }
        else
        {
            camFollow.enabled = false;

        }
        
    }

    public void SettingCameraEnable(bool _check)
    {
        isInteraction = _check;
    }

    private void LateUpdate()
    {

    }
}
