using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater
{
    const float threshold = 0.5f;
    const float rate = 0.25f;
    float next;
    bool hold;
    string axis;

    public Repeater(string axisName)
    {
        axis = axisName;
    }
    public int Update()
    {
        int returnValue = 0;
        int value = Mathf.RoundToInt(Input.GetAxis(axis));

        if (value != 0)
        {
            if (Time.time > next)
            {
                returnValue = value;
                next = Time.time + (hold ? rate : threshold);
                hold = true;
            }
            else
            {
                hold = false;
                next = 0;
            }
        }


        return returnValue;
    }
}

public class KeyBoardInputController : MonoBehaviour 
{
    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Virtical");

    
}
