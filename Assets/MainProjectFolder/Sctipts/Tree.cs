using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour, IDamage
{
    [SerializeField] int tree_HP;

    [SerializeField] float destroy_Time;

    SphereCollider col;

    [SerializeField] GameObject go_Tree;

    [SerializeField] GameObject go_Debris;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
    }

    public void Damage(int playerHit)
    {
        tree_HP -= playerHit;
        
        if(tree_HP <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        col.enabled= false;
        Destroy(go_Tree);

        go_Debris.SetActive(true);
        Destroy(go_Tree, destroy_Time);

    }
}
