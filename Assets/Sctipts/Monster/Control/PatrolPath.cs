using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float flexibleRange = 0.5f;
        const float waypointGizmoRadius = .3f;
        [SerializeField] float Dist = 5.0f;


        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);

                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public int GetNextIndexRandom(int i)
        {
            if (i + 1 == transform.childCount)
            {
                return 0;
            }
            return i + 1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public Vector3 ChangeWaypoint(int i)
        {
            return transform.GetChild(i).position += new Vector3(Random.Range(flexibleRange, -(flexibleRange)), 0.0f, Random.Range(flexibleRange, -(flexibleRange)));
        }

    }
}