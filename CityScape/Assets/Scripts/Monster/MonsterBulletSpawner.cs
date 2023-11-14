using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletSpawner : MonoBehaviour
{
    /* �Ѿ� ���� */
    public float BulletSpawnTime;
    public Vector3 FixPos;

    /* ���� ���� */
    GameObject MyMonster;
    public bool isMonsterLive;

    /* �ʱ�ȭ */
    private void Awake()
    {
        MyMonster = gameObject;
    }

    /* ���Ͱ� ���� ���·� �����ϰ�, �Ѿ� ���� ��ƾ�� ������ */
    private void Start()
    {
        isMonsterLive = true;

        StartCoroutine(SpawnRoutine());
    }

    /* �Ѿ� ���� ��ƾ �Լ� */
    IEnumerator SpawnRoutine()
    {
        while (isMonsterLive) // ���� ���� ��츸 �Ѿ� ����
        {
            GameObject MonsterBullet = PoolManager.GetObject(1);
            // Pool�� 1��° ������(�Ѿ�)�� ������. ���߿� �ش� �κ� ���� ���ɼ� ����
            MonsterBullet.transform.position = MyMonster.transform.position + FixPos; // �Ѿ� ���� ��ġ ����

            yield return new WaitForSeconds(BulletSpawnTime); // SpawnColltime�� ��ŭ ��� �� ����
        }
    }

    /* ���� ��� ���� üũ */
    private void Update() 
    {
        if (MyMonster != null){
            Monster MonsterObj = MyMonster.GetComponent<Monster>();

            if (MonsterObj != null){
                isMonsterLive = MonsterObj.isLive; // Monster ������Ʈ�� isLive ���� ��������
            }
        }
    }
}
