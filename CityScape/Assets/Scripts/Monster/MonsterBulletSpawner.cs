using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletSpawner : MonoBehaviour
{
    GameObject MyMonster;
    GameObject TestMonster;

    public Vector3 FixPos; // �Ѿ� ���� ��ġ ����

    public float WaitForApeear;
    public float SpawnColltime;

    public bool isMonsterLive;

    private void Awake()
    {
        MyMonster = gameObject;
    }

    private void Start()
    {
        isMonsterLive = true; // Monster�� ���� ���·� ����

        // TestMonster = PoolManager.GetObject(2); // ���� �ڵ�

        StartCoroutine(SpawnRoutine());

    }

    IEnumerator SpawnRoutine() // �Ѿ� ���� ��ƾ
    {
        yield return new WaitForSeconds(WaitForApeear);

        while (isMonsterLive) // ���� ���� ��츸 �Ѿ� ����
        {
            GameObject MonsterBullet = PoolManager.GetObject(1);
            MonsterBullet.transform.position = MyMonster.transform.position + FixPos;
            yield return new WaitForSeconds(SpawnColltime); // BulletSpawnTime �� ��ŭ ��� �� ����
        }
    }

    private void Update() // ���� ��� ���� üũ
    {
        if (MyMonster != null)
        {
            Monster MonsterObj = MyMonster.GetComponent<Monster>();
            if (MonsterObj != null)
            {
                isMonsterLive = MonsterObj.isLive; // Monster ������Ʈ�� isLive ���� ��������
            }
        }
    }

}
