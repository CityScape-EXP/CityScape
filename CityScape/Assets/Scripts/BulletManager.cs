using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // �ν����Ϳ��� �ʱ�ȭ
    public GameObject Player;

    public float BulletSpawnTime;

    private void Start()
    {
        StartCoroutine(SpawnRoutine(GameManager.instance.pool.Get(0)));
    }

    IEnumerator SpawnRoutine(Object Obj)
    {
        while (true)
        {
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
            Instantiate(Obj, transform.position, Quaternion.identity); // ������Ʈ ����
        }
    }
}
