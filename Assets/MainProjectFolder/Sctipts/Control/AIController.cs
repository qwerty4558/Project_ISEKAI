using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using RPG.Resources;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        public static AIController instance;

        private float knockbackTimer = 4f; // 넉백 딜레이 시간을 측정하는 타이머


        //수정 가능한 변수(플레이어 인식범위)
        [SerializeField] float chaseDistance = 10f;
        [SerializeField] float suspicionTime = 3f;

        //수정 가능한 변수(정찰 경로 관련)
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointRange = 1f;
        [SerializeField] float waypointDelay = 3f;

        [Range(0, 1)]
        [SerializeField] float patrolSpeedFaction = 0.2f;

        //컴포넌트 변수
        GameObject player;
        Health health;
        Fighter fighter;
        Mover mover;

        private Rigidbody rb;

        //몬스터의 기존 위치
        Vector3 guardPosition;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedPath = Mathf.Infinity;

        int currentWaypointIndex = 0;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();

            guardPosition = transform.position;

            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            instance = this;

           /* if (health.IsDead()) return; */

            fighter.isKnockbacked = false;


            //사거리 내에 범위에 플레이어가 감지 혹은 넉백 상태가 아니게 되면 추적 - 이동, 플레이어가 벗어나면 정지
            if (IsInAttackRange() && fighter.CanAttack(player) && !fighter.isKnockbacked)
            {
                AttackBehaviour();
            }
            //사거리를 벗어나도, 잠시동안 플레이어를 주시하며 경계
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            //경계 시간이 지나면, 원래 있던 위치(스폰 위치)로 복귀
            else
            {
                PatrolBehaviour();
            }
            // 넉백 상태인 경우 타이머를 재서 넉백이 아닌 상태로 되돌림
            if (fighter.isKnockbacked)
            {
                knockbackTimer += Time.deltaTime;
                if (knockbackTimer >= fighter.knockbackTime)
                {
                    fighter.isKnockbacked = false;
                    knockbackTimer = 0f;
                }
            }

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


        //플레이어가 사거리를 벗어나면 정지하고, 경계 상태로 진입
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            transform.LookAt(player.transform);
        }

        //공격 행동
        private void AttackBehaviour()
        {
            fighter.Attack(player);
            timeSinceLastSawPlayer = 0;
        }

        //플레이어와 거리 계산
        private bool IsInAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        //추적, 공격범위 시각화
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }


    }
}


    
