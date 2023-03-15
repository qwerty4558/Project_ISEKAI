using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

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
        StopAllCoroutines();
        StartCoroutine(Cor_MoveScene(to_Scene_Name, Destination_Point_ID));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Move_Scene();
    }
    private IEnumerator Cor_MoveScene(string SceneName, string destPointID)
    {
        DontDestroyOnLoad(gameObject);
        if (destPointID != string.Empty)
        {
            PortalDestinationPoints portalDest;

            yield return StartCoroutine(LoadingSceneController.Instance.YieldLoadScene(SceneName));

            if (FindObjectOfType<PortalDestinationPoints>())
            {
                portalDest = FindObjectOfType<PortalDestinationPoints>();
                portalDest.SetPlayerDestPosition(destPointID);

            }
        }
        Destroy(gameObject);
    }
}

