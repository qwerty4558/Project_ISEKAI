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
        //���� ������ ����(�÷��̾� �νĹ���)
        [SerializeField] float chaseDistance = 10f; // �����Ÿ�
        [SerializeField] float suspicionTime = 3f; // ���� ��ġ�� ���� ���� �ֽ��ϴ� �ð�

        //���� ������ ����(���� ��� ����)
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointRange = 1f;
        [SerializeField] float waypointDelay = 3f;

        [Range(0, 1)]
        [SerializeField] float patrolSpeedFaction = 0.2f;

        //������Ʈ ����
        GameObject player;
        Mover mover;
        Health health;
        Fighter fighter;

        //������ ���� ��ġ
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
        }

        private void Update()
        {
            if (health.IsDead()) return;

            //��Ÿ� ���� ������ �÷��̾ �����Ǹ� ���� - �̵�, �÷��̾ ����� ����
            if (IsInAttackRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            //��Ÿ��� �����, ��õ��� �÷��̾ �ֽ��ϸ� ���
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            //��� �ð��� ������, ���� �ִ� ��ġ(���� ��ġ)�� ����
            else
            {
                PatrolBehaviour();
            }



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
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex); //���� ��� �ε����� ����
            patrolPath.ChangeWaypoint(currentWaypointIndex);
        }

        //������ ��������Ʈ�� ���Ϳ��� �Ÿ� ���
        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointRange;
        }

        //�÷��̾ ��Ÿ��� ����� �����ϰ�, ��� ���·� ����
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
            transform.LookAt(player.transform);
        }

        //���� �ൿ
        private void AttackBehaviour()
        {
            fighter.Attack(player);
            timeSinceLastSawPlayer = 0;
        }

        //�÷��̾�� �Ÿ� ���
        private bool IsInAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        //����, ���ݹ��� �ð�ȭ
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}