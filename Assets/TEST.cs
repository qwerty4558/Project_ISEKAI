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
        if(Input.GetKeyDown(KeyCode.M))
        {
            DiaryController.instance.GetBookInfomation("0,1");
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            DiaryController.instance.GetBookInfomation("1,3");
        }
    }
}
