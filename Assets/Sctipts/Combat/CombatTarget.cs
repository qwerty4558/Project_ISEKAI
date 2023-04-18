using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Resources;

namespace RPG.Combat
{
    //해당 클래스를 가진 오브젝트는 플레이어와 전투를 수행할 수 있는 오브젝트
    //그리고 반드시 체력 클래스를 포함해야 한다.
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
    }
}