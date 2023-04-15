using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WorldToScreen : MonoBehaviour
{
    [SerializeField] private Transform target;
    private RectTransform tf;

    private void Awake()
    {
        tf = GetComponent<RectTransform>();
    }

    void Update()
    {
        tf.position = Camera.main.WorldToScreenPoint(target.position);
    }
}
