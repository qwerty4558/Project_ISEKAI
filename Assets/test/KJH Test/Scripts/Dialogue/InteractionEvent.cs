using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEvent : MonoBehaviour
{
    private void Update()
    {
        
    }

    void DebugDialogue(Dialogue[] talkDatas)
    {
        for (int i = 0; i < talkDatas.Length; i++)
        {
            // ĳ���� �̸� ���
            Debug.Log(talkDatas[i].name);
            // ���� ���
            foreach (string context in talkDatas[i].context)
                Debug.Log(context);
        }
    }
}