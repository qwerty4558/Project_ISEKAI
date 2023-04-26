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
        if(!isInteraction)
        {
            camFollow.enabled = true ;
        }
        else camFollow.enabled = false;
    }

    private void LateUpdate()
    {
    }
}
