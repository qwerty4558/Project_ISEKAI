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

        //사망 체크
        public bool IsDead()
        {
            return isDead;
            
        }

        //데미지 계산
        public void TakeDamage(GameObject instigator, float damage)
        {
            health = Mathf.Max(health - damage, 0);
            print("체력 : " + health);

            if (health <= 0)
            {
                Dead();
                /*
                AwardExperience(instigator);
                */
            }
        }

        //체력 퍼센트 반환
        public float GetPercentage()
        {
            return (health / GetComponent<BaseStats>().GetStat(Stat.Health)) * 100;
        }


        //사망 처리
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

        //경험치 획득
        /*
        private void AwardExperience(GameObject instigator)
        {
            Experience exprience = instigator.GetComponent<Experience>();
            if (exprience == null) return;

            exprience.GainExp(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }
        */


        //세이브 로드
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