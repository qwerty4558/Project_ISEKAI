using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] string to_Scene_Name;
    [SerializeField] string Destination_Point_ID;
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
        LoadingSceneController.Instance.LoadScene(to_Scene_Name);
        MultisceneDatapass.Instance.PortalDestinationID = Destination_Point_ID;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Move_Scene();
    }
}

