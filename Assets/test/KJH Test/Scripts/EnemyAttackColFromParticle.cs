using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackColFromParticle : MonoBehaviour
{
    [SerializeField] private float damege;

    public float Damage { get => damege; set { damege = value; } }
}
