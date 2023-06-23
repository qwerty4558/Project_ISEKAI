using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS_Witch : MonoBehaviour
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



    [SerializeField] bool isAction;
    [SerializeField] bool isHit;
    [SerializeField] bool isFaint;
    [SerializeField] bool isAttack;
    [SerializeField] bool isBossStart;
    [SerializeField] bool hasSpawnMagicStone;
    [SerializeField]
    bool[] magicStoneBreak;

    [SerializeField] GameObject bossDefPos;

    [SerializeField] GameObject witchAttack_1;
    [SerializeField] GameObject witchAttack_2;
    [SerializeField] GameObject witchAttack_3;

    [SerializeField] GameObject midasTree;


    [SerializeField] float targetHeight;
    [SerializeField] float upPositionDuration;
    float _faintTime = 10f;

    [SerializeField]
    GameObject[] magicStone;
    [SerializeField] int stoneCount;

    [SerializeField] Animator animator;
    [SerializeField] Outline outline;

    [SerializeField] UnityEngine.UI.Image hpBar;


    void Awake()
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
        hasSpawnMagicStone = false;
        stoneCount = magicStone.Length;
        magicStoneBreak = new bool[stoneCount];
        WitchBossStart();
    }

    private void WitchBossStart()
    {
        StartCoroutine(CO_Flying_Witch());
        isBossStart = true;
    }

    IEnumerator CO_Flying_Witch()
    {
        if (!isFaint)
        {
            float _startY = transform.position.y;

            float elapsedTime = 0f;


            while (elapsedTime < upPositionDuration)
            {
                float newY = Mathf.Lerp(_startY, targetHeight, elapsedTime / upPositionDuration);

                transform.position = new Vector3(transform.position.x, newY, transform.position.z);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = new Vector3(transform.position.x, targetHeight, transform.position.z);
            yield return null;
        }
        

    }

    IEnumerator CO_Drop_Witch()
    {
     
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
        
        yield return new WaitForSeconds(10f);
        isFaint = false;
        StartCoroutine(CO_Flying_Witch());
    }

    private void MagicStoneSpawner()
    {
        float groundHeight = 0f;
        float minDistance = 5f; // 오브젝트들 간의 최소 거리
        groundHeight -= midasTree.transform.position.y;
        for (int i = 0; i < stoneCount; ++i)
        {
            bool isValidPosition = false;
            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;

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
                for (int j = 0; j < i; j++)
                {
                    if (Vector3.Distance(spawnPos, magicStone[j].transform.position) < minDistance)
                    {
                        isValidPosition = false;
                        break;
                    }
                }
            }

            magicStoneBreak[i] = true;
            GameObject spawnObj = Instantiate(magicStone[i], spawnPos, spawnRot);

            
        }
        hasSpawnMagicStone = true;
    }



    public void StoneBreakCheck(int index)
    {
        magicStoneBreak[index] = false;
        
    }

    private bool CheckAllStoneBreak()
    {
        foreach (bool stoneBreak in magicStoneBreak)
        {
            if (stoneBreak) return false;
        }

        return true;
    }



    // Update is called once per frame
    void Update()
    {
        
        if (isBossStart)
        {
            
            if (!isFaint && !hasSpawnMagicStone)
            {
                MagicStoneSpawner();        
            }
            else
            {
                
                StartCoroutine(CO_Drop_Witch());
            }
            isFaint = CheckAllStoneBreak();
        }
       
    }

  
}
