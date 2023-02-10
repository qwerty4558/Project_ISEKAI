using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tree : MonoBehaviour, IDamage
{
    [SerializeField] float tree_HP;

    [SerializeField] float destroy_Time;

    SphereCollider col;

    [SerializeField] GameObject go_Tree;

    [SerializeField] GameObject go_Debris;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
    }

    private void Destruction()
    {
        GameManager gamemanager = FindObjectOfType<GameManager>();

        gamemanager.colected_Count--;
        col.enabled= false;
        Destroy(go_Tree);
        
        go_Debris.SetActive(true);
        Destroy(go_Tree, destroy_Time);

    }

    public void Hit(float damage)
    {
        tree_HP -= damage;

        if (tree_HP <= 0)
        {
            Destruction();
        }
    }

    public void Damage(float damage)
    {
        Debug.Log("Player Hit Tree");
        Hit(damage);
    }
}
