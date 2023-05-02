using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryScript : MonoBehaviour
{
    public GameObject PediaPanel;
    private bool state;  

    // Start is called before the first frame update
    void Start()
    {
        PediaPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ViewPedia()
    {
        PediaPanel.SetActive(true);
    }
}
