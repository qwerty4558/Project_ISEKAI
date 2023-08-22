using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;


public enum PATTERN_BOSS
{
    Attack = 0,
    Puzzle
}

public class BOSS_Witch : SerializedMonoBehaviour
{
    PlayerController player;
    public List<WitchState> states = new List<WitchState>();
    public PATTERN_BOSS pat;

    [SerializeField] float maxHP = 100f;
    [SerializeField] float currentHP;
    [SerializeField] float attackRange;
    [SerializeField] float attackSpeed;
    [SerializeField] float attackTime;
    [SerializeField] float faintTimeDelay;

    [SerializeField] float spawnStoneRidius = -10f;

    [SerializeField] bool isPuzzleSet = false;


    [SerializeField] bool isAction;
    [SerializeField] bool isHit;
    [SerializeField] bool isFaint;
    [SerializeField] bool isAttack;
    [SerializeField] bool isBossStart;
    [SerializeField] bool hasSpawnMagicStone;
    [SerializeField]
    bool[] magicStoneBreak;

    [SerializeField] GameObject timer;

    [SerializeField] GameObject midasTree;
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject startBoss;

    [SerializeField] GameObject attack_Prefab1;
    [SerializeField] GameObject attack_Prefab2;
    [SerializeField] GameObject attack_Prefab3;
    

    [SerializeField] float targetHeight;
    [SerializeField] float upPositionDuration;
    [SerializeField] float dropPositionDuration = 2f;
    float _faintTime = 10f;

    [SerializeField] GameObject parant_MagicStone;
    [SerializeField] GameObject parant_Attack;
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


    private bool isAttackActive = false;


    [SerializeField] UnityEvent clearEvent;

    void Start()
    {
        if (player == null)
            player = PlayerController.instance;
        timer.SetActive(false);
        stoneCount = magicStone.Length;
        cap = GetComponent<CapsuleCollider>();
        spawn_Interactable = new GameObject[magicStone_Interactable.Length];
        magicStoneBreak = new bool[stoneCount];
        isBossStart = false;
        isPuzzleSet = false;
        animator = GetComponent<Animator>();
        InitWitchBoss();
    }

    private void InitWitchBoss()
    {
        
        bossDotween.DORestartById("start");
        pat = PATTERN_BOSS.Attack;
        currentHP = maxHP;
        transform.position = spawn.transform.position;
        hasSpawnMagicStone = false;

        
        isFaint = false;
        WitchBossStart();
    }

    private void WitchBossStart()
    {
        StartCoroutine(Walk_To_StartPos());
            
    }

    public void ReStart()
    {
        StopAllCoroutines();

        if (timer.activeSelf)
        {
            timer.SetActive(false);
        }

        
        for (int i = 0; i< parant_MagicStone.transform.childCount; i++)
        {
            Destroy(parant_MagicStone.transform.GetChild(i).gameObject);
        }
        for(int j = 0; j < parant_Attack.transform.childCount; j++)
        {
            Destroy(parant_Attack.transform.GetChild(j).gameObject);
        }

        InitWitchBoss();
    }
    IEnumerator Walk_To_StartPos()
    {
        animator.SetTrigger("Start_Boss");
        float startZ = transform.position.z;
        float elapsedTime = 0f;
        float duratioon = 1f;
        while(elapsedTime < duratioon)
        {
            float newZ = Mathf.Lerp(startZ, startBoss.transform.position.z, elapsedTime / duratioon);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isBossStart = true;

        StartCoroutine(CO_Flying_Witch());
    }
    IEnumerator CO_Flying_Witch()
    {
        float _startY = transform.position.y;
        float elapsedTime = 0f;
        isFaint = false;
        cap.enabled = false;
        if (animator != null)
        {
            animator.SetBool("IsFlying", true);
            animator.SetBool("IsFalling", false);
        }
            
        while (elapsedTime < upPositionDuration)
        {
            float newY = Mathf.Lerp(_startY, targetHeight, elapsedTime / upPositionDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        MagicStoneSpawner();
        
        transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
        hasSpawnMagicStone = true; // 상승 완료 후에 생성된 오브젝트 플래그를 true로 설정

        isAttackActive = true;
        yield return new WaitForSeconds(0.2f);
        if(pat == PATTERN_BOSS.Attack)
            StartCoroutine(Attack_To_Player());
    }

    IEnumerator CO_Drop_Witch()
    {
        animator.SetBool("IsAttack1", false);
        animator.SetBool("IsAttack2", false);
        animator.SetBool("IsFalling", true);
        animator.SetBool("IsFlying", false);

        StopCoroutine(Attack_To_Player());
        
        float startY = transform.position.y;
        float elapsedTime = 0f;
        
        while (elapsedTime < dropPositionDuration)
        {
            float newY = Mathf.Lerp(startY, 3.5f, elapsedTime / dropPositionDuration);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
        cap.enabled = true;
        animator.SetBool("IsOnGeoundAfterFall", true);
        yield return new WaitForSeconds(2.5f);

        animator.SetBool("IsOnGeoundAfterFall", false);

        yield return new WaitForSeconds(faintTimeDelay);
        
        hasSpawnMagicStone = false;
        StartCoroutine(CO_Flying_Witch());

        
        
    }
    private void MagicStoneSpawner()
    {
        float groundHeight = 3.5f;
        float minDistance = 10f; // 오브젝트들 간의 최소 거리
        groundHeight -= midasTree.transform.position.y;

        magicStoneObjects.RemoveAll(obj => obj == null);
        magicStoneObjects_Puzzle.RemoveAll(obj => obj == null);
        if (pat == PATTERN_BOSS.Puzzle)
        {
            if(magicStoneObjects_Puzzle.Count == 0)
            {
                for(int i = 0; i < stoneCount; ++i)
                {
                    bool isValidPosition = false;
                    Vector3 spawnPos = Vector3.zero;
                    Quaternion spawnRot = Quaternion.Euler(0f, 180f, 0f);

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
                    spawnobj.transform.parent = parant_MagicStone.transform;
                    magicStoneObjects_Puzzle.Add(spawnobj);
                }
                timer.SetActive(true);
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
        else if(pat == PATTERN_BOSS.Attack)
        {
            if (magicStoneObjects.Count == 0)
            {
                for (int i = 0; i < stoneCount; ++i)
                {
                    bool isValidPosition = false;
                    Vector3 spawnPos = Vector3.zero;
                    Quaternion spawnRot = Quaternion.Euler(0f, 180f, 0f);

                    // 겹치지 않는 위치 찾기
                    while (!isValidPosition)
                    {
                        float randomAngle = Random.Range(0f, 180f); // 0도부터 180도 사이의 랜덤한 각도
                        float angle = randomAngle * Mathf.Deg2Rad;
                        float x = Mathf.Cos(angle) * spawnStoneRidius;
                        float z = Mathf.Sin(angle) * spawnStoneRidius;
                        spawnPos = midasTree.transform.position + new Vector3(x, groundHeight, z);

                        isValidPosition = true; // 초기 값은 유효한 위치라고 가정

                        // 이미 생성된 오브젝트와 거리 비교
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
                    spawnObj.transform.parent = parant_MagicStone.transform;
                    magicStoneObjects.Add(spawnObj); // 생성된 오브젝트를 리스트에 추가
                }
            }
            else
            {
                // 이미 생성된 오브젝트들을 활성화하고 위치 조정
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

            // 이미 생성된 오브젝트와 거리 비교
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
            

            if (pat == PATTERN_BOSS.Puzzle)
            {
                timer.SetActive(false);
                pat = PATTERN_BOSS.Attack;
            }
            else if (pat == PATTERN_BOSS.Attack)
            {
                StopCoroutine(Attack_To_Player());
                StopCoroutine(CO_Attack_Pattern_1());
                StopCoroutine(CO_Attack_Pattern_2());

                for (int j = 0; j < parant_Attack.transform.childCount; j++)
                {
                    Destroy(parant_Attack.transform.GetChild(j).gameObject);
                }
                pat = PATTERN_BOSS.Puzzle;
            }
            
            StartCoroutine(CO_Drop_Witch());
        }
    }

    public void BreakStonePuzzle(int index)
    {
        Destroy(spawn_Interactable[index]);
        magicStoneBreak[index] = false;
        if (CheckAllStoneBreak())
        {
            states[(int)WITCH_STATES.Phase].Action(this);
            
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
        StopAllCoroutines();

        for (int i = 0; i < parant_MagicStone.transform.childCount; i++)
        {
            Destroy(parant_MagicStone.transform.GetChild(i).gameObject);
        }
        for (int j = 0; j < parant_Attack.transform.childCount; j++)
        {
            Destroy(parant_Attack.transform.GetChild(j).gameObject);
        }

        if (clearEvent != null) 
            clearEvent.Invoke();
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
        animator.SetBool("IsAttack1", true);
        GameObject instanceLaser = Instantiate(attack_Prefab1, transform.position, transform.rotation);
        instanceLaser.transform.parent = parant_Attack.transform;
        
        
        yield return new WaitForSeconds(5f);
        animator.SetBool("IsAttack1", false);
        StartCoroutine(Attack_To_Player());
    }
    IEnumerator CO_Attack_Pattern_2() // explosion
    {
        yield return new WaitForSeconds(0.3f);
        

        for (int i = 0; i < 5; ++i)
        {
            if (isAttackActive)
            {
                Transform target = PlayerController.instance.transform;
                Vector3 playerPosition = target.position;
                GameObject instanceExplosion = Instantiate(attack_Prefab2, playerPosition, Quaternion.identity);
                instanceExplosion.transform.parent = parant_Attack.transform;
                yield return new WaitForSeconds(1.5f);
            }
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
