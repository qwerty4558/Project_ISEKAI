using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IDamage
{

    [SerializeField] float monster_Hp = 5f;
    [SerializeField] float monster_Attack_Damage = 1f;
    [SerializeField] float destroy_Time;



    SphereCollider col;


    //[SerializeField] GameObject go_Monster;

    //[SerializeField] GameObject go_Drop_Item;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float Damage)
    {
        monster_Hp -= Damage;
        Debug.Log(monster_Hp);
        if(monster_Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
