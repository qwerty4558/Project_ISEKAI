using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] string to_Scene_Name;
    private PlayerController player;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    public void Move_Scene()
    {
        LoadingSceneController.LoadScene(to_Scene_Name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Move_Scene();
    }
}
