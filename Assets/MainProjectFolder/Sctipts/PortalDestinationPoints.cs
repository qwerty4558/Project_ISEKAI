using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDestinationPoints : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<string,Transform> destinationPoints;

    public bool SetPlayerDestPosition(string point_ID)
    {
        Transform pointTF;
        if (destinationPoints.TryGetValue(point_ID, out pointTF))
        {
            pointTF = destinationPoints[point_ID];
            if (PlayerController.Instance == null) return false;
            else
            {
                PlayerController.Instance.transform.position = pointTF.position;
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public Vector3 GetSpawnPoint(string str)
    {
        Transform point;
        if(PlayerController.Instance != null) point = PlayerController.Instance.transform;
        if(destinationPoints.TryGetValue(str,out point))
        {
            return point.position;
        }
        else
        {
            if(point == null) return Vector3.zero;
            else return point.position;
        }
    }
}
