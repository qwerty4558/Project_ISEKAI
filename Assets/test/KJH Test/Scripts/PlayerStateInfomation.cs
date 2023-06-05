using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateInfomation
{
    public float HP;
    public float playerAttackDamage;
    public string nowScene;

    public List<PlayerAction> playerAction;
    public string nowAction;

}
