using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Resources;
using RPG.Control;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction /*, ISaveable */
    {
        public float knockbackForce = 10f;  // �˹� ��
        public float knockbackDelay = 2f;   // �˹� ������ �ð�
        public float knockbackTime = 0.5f;  // �˹� ���� �ð�
        public bool isKnockbacked = false; // ���� �˹� �������� ����

        private float knockbackDelayTimer = 0f; // �˹� ������ �ð��� �����ϴ� Ÿ�̸�

        public Rigidbody rb; 

        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponRange = 1f;
        [SerializeField] GameObject player;
        /* [SerializeField] GameObject player; */

        /* [SerializeField] Transform rightHandTransform = null;
         [SerializeField] Transform leftHandTransform = null;
         [SerializeField] Weapon defaultWeapon = null;
         [SerializeField] Weapon currentWeapon = null;  
         [SerializeField] string defaultWeaponName = "Unarmed"; */

        Health target;

        void start()
        {
           rb = transform.GetComponent<Rigidbody>(); // Rigidbody ������Ʈ �Ҵ�
        }

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

            //���� -> ���� ���� �ۿ� �ִٸ�, ��Ÿ��� ���� �� ���� �Ѿư�
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

        //���� ������ ���, �ִϸ��̼� Ʈ���� ����, ���� ��ȯ
        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
                transform.LookAt(target.transform);
                Knockback(player.transform);
            }
        }

        //���� ���ۿ� ���� �ִϸ��̼� Ʈ���� ó��
        private void TriggerAttack()
        {
            /*
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
            */
        }

        //Ÿ�ٰ� �Ÿ� ���

        
        private bool GetInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) <= WeaponRange;
        }
        

        //�׼ǽ����췯�� ���� ����� �������̶�� �˸� ����, ���� Ÿ�� ����
        public void Attack(GameObject combatTarget)
        {
            target = combatTarget.GetComponent<Health>();
            GetComponent<ActionScheduler>().StartAction(this);
        }

        //���� ���(ĵ������)
        public void Cancel()
        {
            target = null;
            /* TriggerStopAttack(); */
            GetComponent<Mover>().Cancel();
        }

        //���� ��ҿ� ���� �ִϸ��̼� Ʈ���� ó��
        private void TriggerStopAttack()
        {
            /*
            GetComponent<Animator>().SetTrigger("stopAttack");
            GetComponent<Animator>().ResetTrigger("attack");
            */
        }

        //Ÿ���� ��ȿ�ϸ�, ���� �������� �˻�
        public bool CanAttack(GameObject attackTarget)
        {
            if (attackTarget == null) return false;
            Health targetHealth = attackTarget.GetComponent<Health>();
            return targetHealth != null && targetHealth.IsDead() != true;
        }

        //�ִϸ��̼� �̺�Ʈ ó��(���� ����)
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

        //���Ÿ� ����
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


        public void Knockback(Transform target)
        {
            // �˹� ���°� �ƴ� ��쿡�� ����
            if (!isKnockbacked)
            {
                rb.velocity = Vector3.zero;

                // Enemy ������Ʈ�� Player ������Ʈ�� ��ġ ���̸� ���մϴ�.
                Vector3 direction = (transform.position - target.position).normalized;

                rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
                isKnockbacked = true;
                knockbackDelayTimer = 0f;
            }
        }

        void FixedUpdate()
        {
            // �˹� ������ Ÿ�̸Ӹ� �����Ͽ� ���� �ð��� ������ Knockback �Լ� ����
            if (isKnockbacked)
            {
                knockbackDelayTimer += Time.fixedDeltaTime;
                if (knockbackDelayTimer >= knockbackDelay)
                {
                    // �÷��̾� ������Ʈ�� �˹��մϴ�.
                    Knockback(player.transform);
                }
            }
        }

        //���� �Ⱦ�(���)

        /*
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }


        //���� ������ ���⿡ ���� ���̺� & �ε�
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
            //���ҽ��� ���� ���� ������ ������ �ƴ� ���� �̸��� ���� ã��
            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }
        */
    }
}