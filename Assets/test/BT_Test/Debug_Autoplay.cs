using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Debug_Autoplay : MonoBehaviour
{
    public UnityEvent action;

    private void Start()
    {
        action.Invoke();
    }
}
