using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class testTMP : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI testtext;
    TextMeshPro testMeshPro;

    private void Start()
    {
        TMPpoUGUIDoText.DoText(testtext, 5f);

    }

    private void Update()
    {
    }
    
}