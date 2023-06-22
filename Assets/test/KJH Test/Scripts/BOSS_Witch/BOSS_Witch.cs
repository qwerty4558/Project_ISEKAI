using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Witch : MonoBehaviour
{
    PlayerController player;
    public List<WitchState> states = new List<WitchState>();

    [SerializeField] float maxHP;
    [SerializeField] float currentHP;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackTime;

    [SerializeField] float moveRidius;
    [SerializeField] float moveSpeed;
    [SerializeField] float angle = 0f;


    [SerializeField] bool isAction;
    [SerializeField] bool isHit;
    [SerializeField] bool isAttack;
    [SerializeField] bool isBossStart;

    [SerializeField] GameObject bossDefPos;

    [SerializeField] GameObject witchAttack_1;
    [SerializeField] GameObject witchAttack_2;
    [SerializeField] GameObject witchAttack_3;

    [SerializeField] GameObject midasTree;


    [SerializeField] float targetHeight;
    [SerializeField] float upPositionDuration;
    

    [SerializeField]
    GameObject[] magicStone;

    [SerializeField] Animator animator;
    [SerializeField] Outline outline;

    [SerializeField] UnityEngine.UI.Image hpBar;


    void Start()
    {
        InitWitchBoss();
    }

    private void InitWitchBoss()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
        if (bossDefPos == null)
            bossDefPos = GameObject.FindGameObjectWithTag("BossInitPos");
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        outline = GetComponent<Outline>();

        magicStone = new GameObject[magicStone.Length];

        WitchBossStart();
    }

    private void WitchBossStart()
    {
        StartCoroutine(CO_Flying_Witch());
    }

    IEnumerator CO_Flying_Witch()
    {
        float _startY = transform.position.y;

        float elapsedTime = 0f;


        while(elapsedTime < upPositionDuration)
        {
            float newY = Mathf.Lerp(_startY, targetHeight, elapsedTime / upPositionDuration);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
        yield return null;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isBossStart)
        {
            MoveWitch();
        }
    }

    private void MoveWitch()
    {
        Vector3 center = midasTree.transform.position;

        Vector3 playerToCenter = player.transform.position - midasTree.transform.position;

        Vector3 lomitVector = Vector3.ClampMagnitude(playerToCenter, moveRidius);

        Vector3 newPos = center + lomitVector;

        transform.position = newPos;
    }
}
