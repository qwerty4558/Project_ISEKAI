using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public bool EnableIntroCamera = true;
    public float holdTime = 1.0f;
    public GameObject targetVcam;

    private void Start()
    {
        if(EnableIntroCamera)
        {
            targetVcam.SetActive(true);
            Invoke("DisableCamera", holdTime);
        }
    }

    public void DisableCamera()
    {
        targetVcam.SetActive(false);
    }
}
