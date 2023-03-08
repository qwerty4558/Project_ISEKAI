using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        //���� ������ ����(���� ��� ����)
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointRange = 1f;
        [SerializeField] float waypointDelay = 3f;

        [Range(0, 1)]
        [SerializeField] float patrolSpeedFaction = 0.2f;

        //������Ʈ ����
        Mover mover;

        //������ ���� ��ġ
        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedPath = Mathf.Infinity;

        int currentWaypointIndex = 0;

        private void Start()
        {
            mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        private void Update()
        {
                PatrolBehaviour();

            //�ൿ �ֱ� �ð�, ������ �ð� ������Ʈ
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedPath += Time.deltaTime;
        }

        //���� ��������Ʈ�� �����ϰ�, ��� ��� �� �̵�
        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedPath = 0;
                    CycleWaypoint();
                    Debug.Log("Cycle Way Point");
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedPath > waypointDelay)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFaction);
            }
        }

        //���� ���Ͱ� ���� ��θ� ������ �ִٸ�, ��� ����Ʈ�� �ִ��� ��ȯ
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //���� ��������Ʈ�� ��� ����
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        //������ ��������Ʈ�� ���Ϳ��� �Ÿ� ���
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointRange;
        }

    }
}