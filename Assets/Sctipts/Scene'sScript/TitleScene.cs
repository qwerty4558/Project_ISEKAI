using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public Setting settings;
    // Start is called before the first frame update
    void Start()
    {
        settings = FindObjectOfType<Setting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickButton(int btnNum)
    {
        switch (btnNum)
        {

            case 0:
                LoadingSceneController.Instance.LoadScene("L_Main");
                break;
            case 1:
                settings.mainOpion.gameObject.SetActive(true);
                break;
            case -1:
                Application.Quit();
                break;
        }        
    }
}
