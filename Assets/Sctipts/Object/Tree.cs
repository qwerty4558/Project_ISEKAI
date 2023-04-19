using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour, IPlayerAction.IDamage
{
    [SerializeField] float tree_HP;

    [SerializeField] float destroy_Time;

    SphereCollider col;

    [SerializeField] GameObject go_Tree_Hp;

    [SerializeField] Slider slider_hp;

    [SerializeField] GameObject go_Tree;

    [SerializeField] GameObject go_Debris;

    [SerializeField] Outline _outline;

    float initialTreeHP;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
        initialTreeHP = tree_HP;
    }

    void Update()
    {
        _outline.OutlineWidth = 2f;
    }

    private void Destruction()
    {
        //GameManager.Instance.colected_Count--;
        col.enabled = false;
        Destroy(go_Tree);

        go_Debris.SetActive(true);
        Destroy(go_Tree, destroy_Time);

    }

    public void Hit(float damage)
    {
        tree_HP -= damage;

        go_Tree_Hp.SetActive(true);
        slider_hp.value = tree_HP / initialTreeHP;

        StopAllCoroutines();
        StartCoroutine(Cor_hpUI_Visiablity(5.0f));

        if (tree_HP <= 0)
        {
            Destruction();
        }
    }

    public void Damage(float damage)
    {
        Hit(damage);
    }

    private IEnumerator Cor_hpUI_Visiablity(float time)
    {
        yield return new WaitForSeconds(time);

        go_Tree_Hp.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (other.GetComponent<ActiveAttackCol>().PlayerActionState != ActionState.Axe) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            Damage(tempDamage);
        }
    }
}