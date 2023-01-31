using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CircleGauge : ItemManager
{
    public Image progress_Circle;
    private float current_Value;
    public float speed;

    public int total_Value;
    public int main_Count;
    public int sub_Count;

    // Start is called before the first frame update
    void Start()
    {
        total_Value = main_Count + sub_Count;
        ViewItem();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        ProgressGauge();
        
    }

    public void ReadResultItemID(int id1, int id2)
    {
        int result_List_Count = result_List.Count;

        for(int i = 0; i< result_List_Count; ++i)
        {

        }
    }

    private void ProgressGauge()
    {
        if (current_Value < total_Value)
        {
            current_Value += speed * Time.deltaTime;
        }
        else
        {

        }
        progress_Circle.fillAmount = current_Value / total_Value;
    }
}
