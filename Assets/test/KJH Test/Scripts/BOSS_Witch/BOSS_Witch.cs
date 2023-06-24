using DG.Tweening;
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
    [SerializeField] DOTweenAnimation bossDotween;

    private List<GameObject> magicStoneObjects = new List<GameObject>();

    private CapsuleCollider cap;
    private float outlineDelay;

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
        InitWitchBoss();
    }

    private void InitWitchBoss()
    {
        bossDotween.DORestartById("start");
        currentHP = maxHP;
        
        hasSpawnMagicStone = false;
       
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
        cap.enabled = true;
        yield return new WaitForSeconds(faintTimeDelay);
        hasSpawnMagicStone = false;
        StartCoroutine(CO_Flying_Witch());

        
        
    }

    private void MagicStoneSpawner()
    {
        float groundHeight = 0f;
        float minDistance = 5f; // ������Ʈ�� ���� �ּ� �Ÿ�
        groundHeight -= midasTree.transform.position.y;

        magicStoneObjects.RemoveAll(obj => obj == null);

        // �̹� ������ ������Ʈ�� ���ٸ� ������Ʈ���� �����ϰ� ����Ʈ�� ����
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

        // ��� �����̰� �ı��Ǿ����� Ȯ��
        if (CheckAllStoneBreak())
        {
            StartCoroutine(CO_Drop_Witch());
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
    // Update is called once per frame
    void Update()
    {
        if (isBossStart)
        {
            HpUpdate();
        }
        
    }

}
