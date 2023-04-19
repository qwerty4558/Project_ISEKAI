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
    //ISavable -> 세이브 로드 시스템에 저장 될 수 있는 인터페이스로 등록
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

        //플레이어 캐릭터의 이동속도 z값을 로컬좌표 기준으로 변환시킨 다음, 애니메이터에 수치 동기화
        //이동속도에 따라서 애니메이션의 움직임이 바뀐다(blend tree)
        private void UpdateAnimator()
        {
            Vector3 velocity = nav.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            currentSpeed = localVelocity.z;
            /* GetComponent<Animator>().SetFloat("forwardSpeed", (currentSpeed)); */
        }

        //네비게이션 매쉬를 통한 움직임을 위해 네비매쉬의 목적지 설정
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

        //액션 스케쥴러에 이동 액션중이라고 알림
        public void StartMoveAction(Vector3 destination, float fraction = 1f)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, fraction);
        }

        //구조체를 사용한 매개변수 캡처
        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        //현재 위치를 저장
        public object CaptureState()
        {
            //딕셔너리 사용한 매개변수 전달
            /*Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;*/

            //구조체를 사용한 매개변수 전달
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;

        }

        //저장된 위치를 불러옴
        public void RestoreState(object state)
        {
            //딕셔너리 사용한 매개변수 전달
            /*Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true;*/

            //구조체를 사용한 매개변수 전달
            MoverSaveData data = (MoverSaveData)state;

            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}