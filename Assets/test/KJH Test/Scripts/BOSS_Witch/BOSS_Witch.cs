using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

public class BOSS_Witch : SerializedMonoBehaviour
{
    PlayerController player;
    public List<WitchState> states = new List<WitchState>();

    [SerializeField] float maxHP = 100f;
    [SerializeField] float currentHP;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackTime;
    [SerializeField] float faintTimeDelay;

    [SerializeField] float spawnStoneRidius = 10f;

    [SerializeField] bool isPuzzleSet = false;


    [SerializeField] bool isAction;
    [SerializeField] bool isHit;
    [SerializeField] bool isFaint;
    [SerializeField] bool isAttack;
    [SerializeField] bool isBossStart;
    [SerializeField] bool hasSpawnMagicStone;
    [SerializeField]
    bool[] magicStoneBreak;

    [SerializeField] GameObject bossDefPos;

    [SerializeField] GameObject midasTree;

    [SerializeField] GameObject attack_Prefab1;
    [SerializeField] GameObject attack_Prefab2;
    [SerializeField] GameObject attack_Prefab3;
    

    [SerializeField] float targetHeight;
    [SerializeField] float upPositionDuration;
    float _faintTime = 10f;

    [SerializeField]
    GameObject[] magicStone;
    [SerializeField]
    GameObject[] magicStone_Interactable;
    [SerializeField] GameObject[] spawn_Interactable;
    [SerializeField] int stoneCount;

    [SerializeField] Animator animator;
    [SerializeField] Outline outline;

    [SerializeField] UnityEngine.UI.Image hpBar;
    [SerializeField] DOTweenAnimation bossDotween;

    private List<GameObject> magicStoneObjects = new List<GameObject>();
    private List<GameObject> magicStoneObjects_Puzzle = new List<GameObject>();

    private CapsuleCollider cap;
    private float outlineDelay;

    int current_State_Index;
    private bool isAttackActive = false;

    [SerializeField] UnityEvent clearEvent;

    void Start()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();
        if (bossDefPos == null)
            bossDefPos = GameObject.FindGameObjectWithTag("BossInitPos");
        stoneCount = magicStone.Length;
        cap = GetComponent<CapsuleCollider>();
        magicStoneBreak = new bool[stoneCount];
        isBossStart = true;
        isPuzzleSet = false;
        InitWitchBoss();
    }

    private void InitWitchBoss()
    {
        bossDotween.DORestartById("start");
        currentHP = maxHP;
        current_State_Index = 1;
        hasSpawnMagicStone = false;
        states[current_State_Index].OnEnterAction(this);
        spawn_Interactable = new GameObject[magicStone_Interactable.Length];
        isFaint = false;
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
        isFaint = false;
        cap.enabled = false;

        if (current_State_Index <= 2) isPuzzleSet = true;

        while (elapsedTime < upPositionDuration)
        {
            float newY = Mathf.Lerp(_startY, targetHeight, elapsedTime / upPositionDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        MagicStoneSpawner();
        
        transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
        hasSpawnMagicStone = true; // ��� �Ϸ� �Ŀ� ������ ������Ʈ �÷��׸� true�� ����

        isAttackActive = true;
        if(current_State_Index == 1 || current_State_Index == 3)
            StartCoroutine(Attack_To_Player());
    }

    IEnumerator CO_Drop_Witch()
    {
        StopCoroutine(Attack_To_Player());

        float startY = transform.position.y;
        float elapsedTime = 0f;

        while (elapsedTime < upPositionDuration)
        {
            float newY = Mathf.Lerp(startY, 0f, elapsedTime / upPositionDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        cap.enabled = true;
        yield return new WaitForSeconds(faintTimeDelay);
        hasSpawnMagicStone = false;
        StartCoroutine(CO_Flying_Witch());

        
        
    }
    private void MagicStoneSpawner()
    {
        float groundHeight = 0f;
        float minDistance = 10f; // ������Ʈ�� ���� �ּ� �Ÿ�
        groundHeight -= midasTree.transform.position.y;

        magicStoneObjects.RemoveAll(obj => obj == null);
        magicStoneObjects_Puzzle.RemoveAll(obj => obj == null);
        if(current_State_Index == 2)
        {
            if(magicStoneObjects_Puzzle.Count == 0)
            {
                for(int i = 0; i < stoneCount; ++i)
                {
                    bool isValidPosition = false;
                    Vector3 spawnPos = Vector3.zero;
                    Quaternion spawnRot = Quaternion.identity;

                    while (!isValidPosition)
                    {
                        float randomAngle = Random.Range(0f, 180f);
                        float angle = randomAngle * Mathf.Deg2Rad;

                        float x = Mathf.Cos(angle) * spawnStoneRidius;
                        float z = Mathf.Sin(angle) * spawnStoneRidius;

                        spawnPos = midasTree.transform.position + new Vector3(x, groundHeight, z);

                        isValidPosition = true;
                        foreach(GameObject obj in magicStoneObjects_Puzzle)
                        {
                            if(Vector3.Distance(spawnPos, obj.transform.position) < minDistance)
                            {
                                isValidPosition = false;
                                break;
                            }
                        }
                    }
                    magicStoneBreak[i] = true;
                    GameObject spawnobj = Instantiate(magicStone_Interactable[i], spawnPos, spawnRot);
                    magicStoneObjects_Puzzle.Add(spawnobj);
                }
            }

            else
            {
                for (int i = 0; i < stoneCount; ++i)
                {
                    if (i < magicStoneObjects_Puzzle.Count)
                    {
                        magicStoneObjects_Puzzle[i].SetActive(true);
                        magicStoneObjects_Puzzle[i].transform.position = CalculateValidPosition(groundHeight, minDistance);
                    }
                }
            }
        }
        else
        {
            if (magicStoneObjects.Count == 0)
            {
                for (int i = 0; i < stoneCount; ++i)
                {
                    bool isValidPosition = false;
                    Vector3 spawnPos = Vector3.zero;
                    Quaternion spawnRot = Quaternion.identity;

                    // ��ġ�� �ʴ� ��ġ ã��
                    while (!isValidPosition)
                    {
                        float randomAngle = Random.Range(0f, 180f); // 0������ 180�� ������ ������ ����
                        float angle = randomAngle * Mathf.Deg2Rad;
                        float x = Mathf.Cos(angle) * spawnStoneRidius;
                        float z = Mathf.Sin(angle) * spawnStoneRidius;
                        spawnPos = midasTree.transform.position + new Vector3(x, groundHeight, z);

                        isValidPosition = true; // �ʱ� ���� ��ȿ�� ��ġ��� ����

                        // �̹� ������ ������Ʈ�� �Ÿ� ��
                        foreach (GameObject obj in magicStoneObjects)
                        {
                            if (Vector3.Distance(spawnPos, obj.transform.position) < minDistance)
                            {
                                isValidPosition = false;
                                break;
                            }
                        }
                    }

                    magicStoneBreak[i] = true;
                    GameObject spawnObj = Instantiate(magicStone[i], spawnPos, spawnRot);
                    magicStoneObjects.Add(spawnObj); // ������ ������Ʈ�� ����Ʈ�� �߰�
                }
            }
            else
            {
                // �̹� ������ ������Ʈ���� Ȱ��ȭ�ϰ� ��ġ ����
                for (int i = 0; i < stoneCount; ++i)
                {
                    if (i < magicStoneObjects.Count)
                    {
                        magicStoneObjects[i].SetActive(true);
                        magicStoneObjects[i].transform.position = CalculateValidPosition(groundHeight, minDistance);
                    }
                }
            }
        }
        
    }

    private Vector3 CalculateValidPosition(float groundHeight, float minDistance)
    {
        Vector3 spawnPos = Vector3.zero;
        bool isValidPosition = false;

        while (!isValidPosition)
        {
            float randomAngle = Random.Range(0f, 180f); 
            float angle = randomAngle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * spawnStoneRidius;
            float z = Mathf.Sin(angle) * spawnStoneRidius;
            spawnPos = midasTree.transform.position + new Vector3(x, groundHeight, z);

            isValidPosition = true;

            // �̹� ������ ������Ʈ�� �Ÿ� ��
            foreach (GameObject obj in magicStoneObjects)
            {
                if (Vector3.Distance(spawnPos, obj.transform.position) < minDistance)
                {
                    isValidPosition = false;
                    break;
                }
            }
        }

        return spawnPos;
    }

    public void StoneBreakCheck(int index)
    {
        magicStoneBreak[index] = false;


        if (CheckAllStoneBreak())
        {
            isAttackActive = false;


            StartCoroutine(CO_Drop_Witch());
        }
    }

    public void BreakStonePuzzle(int index)
    {
        Destroy(spawn_Interactable[index]);
        magicStoneBreak[index] = false;
        if (CheckAllStoneBreak())
        {
            current_State_Index = 3;
            states[(int)WITCH_STATES.Phase].Action(this);
            StartCoroutine(Attack_To_Player());
        }
    }

    private bool CheckAllStoneBreak()
    {
        foreach (bool stoneBreak in magicStoneBreak)
        {
            if (stoneBreak) return false;
        }

        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackCol"))
        {
            if (!other.GetComponent<ActiveAttackCol>().CompareActionType(typeof(Action_Sword))) return;

            float tempDamage = other.GetComponent<ActiveAttackCol>().LinkDamage;
            GetDamage(tempDamage);
            //EffectActive(other.transform);
        }
    }
    private void Dead()
    {
        //bossAnimator.SetTrigger("isDead");
        if (clearEvent != null) clearEvent.Invoke();
    }

    private void Hit()
    {
        bossDotween.DORestartById("hit");
        if (currentHP <= 0) Dead();
        else if (currentHP > 0 && !isAttack)
        {
            /*if (isHit)
                bossAnimator.SetTrigger("isHitExit");
            isHit = true;
            int rand = Random.Range(0, 2);
            if (rand > 0) bossAnimator.SetTrigger("isHit_1");
            else bossAnimator.SetTrigger("isHit_2");*/
        }
    }
    private void HpUpdate()
    {
        hpBar.fillAmount = currentHP / maxHP;


    }
    public void GetDamage(float _damage)
    {
        currentHP -= _damage;
        OutlineActive();
        Hit();

        if (currentHP <= maxHP * 2 / 3 && current_State_Index == 1)
        {
            StartCoroutine(CO_Flying_Witch());
            current_State_Index = 2;
            states[(int)WITCH_STATES.Phase].Action(this);
        }
    }
    private void OutlineActive()
    {
        outlineDelay = 1f;
        outline.enabled = true;
    }


    IEnumerator Attack_To_Player()
    {
        yield return new WaitForSeconds(0.2f);
        if (isAttackActive)
        {
            int index = Random.Range(0, 2);
            switch (index)
            {
                case 0:
                    StartCoroutine(CO_Attack_Pattern_1());
                    break;
                case 1:
                    StartCoroutine(CO_Attack_Pattern_2());
                    break;
            }
            yield return new WaitForSeconds(2f);
        }
    }


    IEnumerator CO_Attack_Pattern_1() // laser
    {
        yield return new WaitForSeconds(0.2f);
        GameObject instanceLaser = Instantiate(attack_Prefab1, transform.position, transform.rotation);


        
        yield return new WaitForSeconds(5f);
        StartCoroutine(Attack_To_Player());
    }
    IEnumerator CO_Attack_Pattern_2() // explosion
    {
        yield return new WaitForSeconds(0.3f);
        

        for (int i = 0; i < 5; ++i)
        {
            Transform target = PlayerController.instance.transform;
            Vector3 playerPosition = target.position;
            GameObject instanceExplosion = Instantiate(attack_Prefab2, playerPosition, Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(Attack_To_Player());
    }

    /*    IEnumerator CO_Attack_Pattern_3() // shot bullet
        {
            yield return null;
            StartCoroutine(Attack_To_Player());
        }*/

    void Update()
    {
        if (isBossStart)
        {
            if (isAttack)
            {
                states[(int)WITCH_STATES.Phase].Action(this);
                
            }
            else
            {
                //states[(int)WITCH_STATES.Faint].Action(this);
            }
            HpUpdate();
        }
        
    }

    public void PuzzleSet()
    {
        isPuzzleSet = false;
    }
}
