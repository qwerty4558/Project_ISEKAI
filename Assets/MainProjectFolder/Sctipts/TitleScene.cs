using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButton(int btnNum)
    {
        if(btnNum == 0)
        {
            GameManager.Instance.colected_Time = 30f; 
            LoadingSceneController.Instance.LoadScene("Village");
        }
        else
        {
            
        }
    }
}
