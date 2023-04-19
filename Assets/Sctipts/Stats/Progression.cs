using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            //��� ĳ���� Ŭ���� Ž��
            foreach (ProgressionCharacterClass progressionClass in this.characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                //��� ���� Ž��
                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    if (progressionStat.stat != stat) continue;

                    if (progressionStat.levels.Length < level) continue;

                    return progressionStat.levels[level - 1];
                }
            }
            return 0;
        }

        //Ŭ����, ����ü�� ����ȭ -> Serializable ���, ���� ������ SerializeField �����ϰų� public���� ����
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}