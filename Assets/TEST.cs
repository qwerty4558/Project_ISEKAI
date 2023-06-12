using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    public QuestTitle q;
    // Start is called before the first frame update
    void Start()
    {
        q.QuestInput("TEST101");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            DiaryController.instance.GetBookInfomation("0,1"); 
            DiaryController.instance.GetBookInfomation("1,1");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            DiaryController.instance.GetBookInfomation("0,2");
            DiaryController.instance.GetBookInfomation("1,2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DiaryController.instance.GetBookInfomation("0,3");
            DiaryController.instance.GetBookInfomation("1,3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DiaryController.instance.GetBookInfomation("0,4");
            DiaryController.instance.GetBookInfomation("1,4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            DiaryController.instance.GetBookInfomation("0,5");
            DiaryController.instance.GetBookInfomation("1,5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            DiaryController.instance.GetBookInfomation("0,6");
            DiaryController.instance.GetBookInfomation("1,6");
        }
    }
}
