using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer_count : MonoBehaviour
{
    public GameObject result_Panel;
    [SerializeField] private GameObject LL;
    public Text scriptTxt;
    public int count = 3;
    void Start()
    {
        InitializeText();
        result_Panel.SetActive(false);
        LL.SetActive(false);

    }
    void InitializeText()
    {
        scriptTxt.text = count.ToString();
    }

    public void Count()
    {
        count--;
        scriptTxt.text = count.ToString();
        Debug.Log("OtherCubeScript public int = " + count);
        if (count <= 0)
        {
            Debug.Log("no chance");
            result_Panel.SetActive(true);
        }

        if (count == 1)
        {
            Debug.Log("no chance");
            LL.SetActive(true);
            Invoke("RemoveIcon", 0.2f);
            
        }
    }

    void RemoveIcon()
    {
        LL.SetActive(false);
    }
}
