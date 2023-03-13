using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    //캐릭터의 레벨, 클래스, 체력 등의 스탯을 담은 클래스
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        
        [SerializeField] int startingLevel = 1;
        
        [SerializeField] CharacterClass characterClass;
        
        [SerializeField] Progression progression = null;
        

        //모든 스탯에 대해 값을 가져오게끔 함수 하나로 통일
        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat , characterClass, startingLevel);
        }
    }
}