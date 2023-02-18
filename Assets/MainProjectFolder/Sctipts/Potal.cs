using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] string to_Scene;
    [SerializeField] string from_Scene;

    public void Move_Scene()
    {
        LoadingSceneController.LoadScene(to_Scene);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        Move_Scene();
    }
}
