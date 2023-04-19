using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Combat;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Movement
{
    //ISavable -> ���̺� �ε� �ý��ۿ� ���� �� �� �ִ� �������̽��� ���
    public class Mover : MonoBehaviour, IAction /*, ISaveable */
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;

        NavMeshAgent nav;
        Health health;

        float currentSpeed = 0f;

        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            /*
            nav.enabled = !health.IsDead();

            UpdateAnimator();
            */
        }

        //�÷��̾� ĳ������ �̵��ӵ� z���� ������ǥ �������� ��ȯ��Ų ����, �ִϸ����Ϳ� ��ġ ����ȭ
        //�̵��ӵ��� ���� �ִϸ��̼��� �������� �ٲ��(blend tree)
        private void UpdateAnimator()
        {
            Vector3 velocity = nav.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            currentSpeed = localVelocity.z;
            /* GetComponent<Animator>().SetFloat("forwardSpeed", (currentSpeed)); */
        }

        //�׺���̼� �Ž��� ���� �������� ���� �׺�Ž��� ������ ����
        public void MoveTo(Vector3 destination, float fraction = 1f)
        {
            nav.isStopped = false;
            nav.speed = maxSpeed * Mathf.Clamp01(fraction);
            nav.destination = destination;
        }

        public void Cancel()
        {
            nav.isStopped = true;
        }

        //�׼� �����췯�� �̵� �׼����̶�� �˸�
        public void StartMoveAction(Vector3 destination, float fraction = 1f)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, fraction);
        }

        //����ü�� ����� �Ű����� ĸó
        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        //���� ��ġ�� ����
        public object CaptureState()
        {
            //��ųʸ� ����� �Ű����� ����
            /*Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;*/

            //����ü�� ����� �Ű����� ����
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;

        }

        //����� ��ġ�� �ҷ���
        public void RestoreState(object state)
        {
            //��ųʸ� ����� �Ű����� ����
            /*Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;*/

            //����ü�� ����� �Ű����� ����
            MoverSaveData data = (MoverSaveData)state;

            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}