using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopScene : MonoBehaviour
{
    [SerializeField] GameObject _craftPuzzle;
    [SerializeField] GameObject _discrimination;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_craftPuzzle != null && _discrimination != null)
        {
            if(_craftPuzzle.activeSelf == true || _discrimination.activeSelf == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState= CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        
    }
}
