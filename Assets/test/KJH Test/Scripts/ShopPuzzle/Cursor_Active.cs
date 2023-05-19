using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Cursor_Active : MonoBehaviour
{
    [SerializeField] GameObject craftPuzzle;
    [SerializeField] AudioMixer audioMixer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(craftPuzzle.activeInHierarchy == true)
        {
            Cursor.visible = true;
            audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            Cursor.visible = false;
            audioMixer.SetFloat("SFX", -10f);
        }
    }
}
