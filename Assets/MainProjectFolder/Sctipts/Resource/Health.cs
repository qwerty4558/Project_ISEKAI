using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* using RPG.Saving; */
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour /*, ISaveable */
    {
        [SerializeField] float health = 100f;
        float maxHealth;
        bool isDead = false;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
            maxHealth = health;
        }

        //��� üũ
        public bool IsDead()
        {
            return isDead;
            
        }

        //������ ���
        public void TakeDamage(GameObject instigator, float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print("ü�� : " + health);

            if (health <= 0)
            {
                Dead();
                /*
                AwardExperience(instigator);
                */
            }
        }

        //ü�� �ۼ�Ʈ ��ȯ
        public float GetPercentage()
        {
            return (health / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
        }


        //��� ó��
        void Dead()
        {
            if (isDead) return;

            isDead = true;
            Debug.Log("RIP");
            /*
            GetComponent<Animator>().SetTrigger("die");
            */
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        //����ġ ȹ��
        /*
        private void AwardExperience(GameObject instigator)
        {
            Experience exprience = instigator.GetComponent<Experience>();
            if (exprience == null) return;

            exprience.GainExp(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        */


        //���̺� �ε�
        public object CaptureState()
        {
            return health;
        }
        public void RestoreState(object state)
        {
            float healthData = (float)state;
            health = healthData;

            if (health <= 0)
            {
                Dead();
            }
        }
    }
}