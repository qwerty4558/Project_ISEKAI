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
        //수정 가능한 변수(정찰 경로 관련)
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointRange = 1f;
        [SerializeField] float waypointDelay = 3f;

        [Range(0, 1)]
        [SerializeField] float patrolSpeedFaction = 0.2f;

        //컴포넌트 변수
        Mover mover;

        //몬스터의 기존 위치
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

            //행동 주기 시간, 딜레이 시간 업데이트
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedPath += Time.deltaTime;
        }

        //다음 웨이포인트를 지정하고, 잠시 대기 후 이동
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

        //현재 몬스터가 정찰 경로를 가지고 있다면, 어느 포인트에 있는지 반환
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        //다음 웨이포인트로 경로 지정
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        //목적지 웨이포인트와 몬스터와의 거리 계산
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointRange;
        }

    }
}