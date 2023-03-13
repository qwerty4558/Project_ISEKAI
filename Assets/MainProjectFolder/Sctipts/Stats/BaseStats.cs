using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    //ĳ������ ����, Ŭ����, ü�� ���� ������ ���� Ŭ����
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        
        [SerializeField] int startingLevel = 1;
        
        [SerializeField] CharacterClass characterClass;
        
        [SerializeField] Progression progression = null;
        

        //��� ���ȿ� ���� ���� �������Բ� �Լ� �ϳ��� ����
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat , characterClass, startingLevel);
        }
    }
}