using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamage
{

    [SerializeField] float monster_Hp = 5f;
    [SerializeField] float monster_Attack_Damage = 1f;
    [SerializeField] float destroy_Time;

    SphereCollider col;

    [SerializeField] GameObject go_Monster;

    [SerializeField] GameObject go_Drop_Item;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float Damage)
    {
        throw new System.NotImplementedException();
    }
}
