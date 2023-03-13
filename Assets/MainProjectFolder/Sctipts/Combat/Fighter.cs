using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction /*, ISaveable */
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponRange = 1f;
        /* [SerializeField] Transform rightHandTransform = null;
         [SerializeField] Transform leftHandTransform = null;
         [SerializeField] Weapon defaultWeapon = null;
         [SerializeField] Weapon currentWeapon = null;  
         [SerializeField] string defaultWeaponName = "Unarmed"; */

        Health target;

        public float WeaponRange
        {
            get { return weaponRange; }
        }
        public Health GetTarget()
        {
            return target;
        }

        float timeSinceLastAttack = Mathf.Infinity;

        Mover mover;

        private void Awake()
        {
            /* currentWeapon = defaultWeapon; */
            mover = GetComponent<Mover>();

           /* if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
           */
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            //공격 -> 공격 범위 밖에 있다면, 사거리에 닿일 때 까지 쫓아감
            if (target == null || target.IsDead()) return;

            if (!GetInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        //공격 딜레이 계산, 애니메이션 트리거 동작, 방향 전환
        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
                transform.LookAt(target.transform);
            }
        }

        //공격 시작에 대한 애니메이션 트리거 처리
        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //타겟과 거리 계산

        
        private bool GetInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) <= WeaponRange;
        }
        

        //액션스케쥴러에 공격 명령을 수행중이라고 알린 다음, 공격 타겟 설정
        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }

        //공격 취소(캔슬동작)
        public void Cancel()
        {
            target = null;
            /* TriggerStopAttack(); */
            GetComponent<Mover>().Cancel();
        }

        //공격 취소에 대한 애니메이션 트리거 처리
        private void TriggerStopAttack()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Animator>().ResetTrigger("attack");
        }

        //타겟이 유효하며, 생존 상태인지 검사
        public bool CanAttack(GameObject attackTarget)
        {
            if (attackTarget == null) return false;
            Health targetHealth = attackTarget.GetComponent<Health>();
            return targetHealth != null && targetHealth.IsDead() != true;
        }

        //애니메이션 이벤트 처리(근접 공격)
        void Hit()
        {
            if (target == null)
            {
                return;
            }
            /*
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject);
            }
            else
            {
                target.TakeDamage(gameObject, currentWeapon.WeaponDamage);
            }
            */
        }

        //원거리 공격
        void Shoot()
        {
            Hit();
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, WeaponRange);
            }
        }

        //무기 픽업(장비)

        /*
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }


        //현재 장착한 무기에 대해 세이브 & 로드
        public object CaptureState()
        {
            if (currentWeapon != null)
            {
                return currentWeapon.name;
            }

            return null;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            //리소스를 통해 직접 프리펩 연결이 아닌 파일 이름을 통해 찾음
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
        */
    }
}