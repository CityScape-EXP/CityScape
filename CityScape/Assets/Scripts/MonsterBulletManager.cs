using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // �ν����Ϳ��� �ʱ�ȭ
    public GameObject Monster;
    public int id;

    public Vector3 FixPos; // �Ѿ� ���� ��ġ ����

    public float BulletWaitTime;
    public float BulletSpawnTime;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(BulletWaitTime);
        while (true)
        {
            GameObject Obj = GameManager.instance.pool.Get(3);
            Obj.transform.position = Monster.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
        }
    }

    public void StopBullet()
    {
        StopCoroutine(SpawnRoutine()); // ���Ͱ� ������ �Ѿ� ���� ��ƾ�� ����
    }
}
