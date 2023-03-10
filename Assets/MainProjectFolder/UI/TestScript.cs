using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    public Transform gameUI;
    bool isUIActive;
    // Start is called before the first frame update
    void Start()
    {
        isUIActive = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isUIActive)
            {
                gameUI.DOMoveX(620, 2);
                isUIActive = true;
            }
            else
            {
                gameUI.DOMoveX(-420, 2);
                isUIActive = false;
            }
        }
    }
}
