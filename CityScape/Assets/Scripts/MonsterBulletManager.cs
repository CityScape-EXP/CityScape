using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // �ν����Ϳ��� �ʱ�ȭ
    public GameObject Monster;
    public int id;

    private Vector3 MonsterPosit;
    public Vector3 FixPos; // �Ѿ� ���� ��ġ ����

    public float BulletSpawnTime;


    private void Start()
    {
        MonsterPosit = Monster.transform.position + FixPos;
        StartCoroutine(SpawnRoutine());
    }


    private void Update()
    {
        MonsterPosit = Monster.transform.position + FixPos;
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject Obj = GameManager.instance.pool.Get(3);
            Obj.transform.position = MonsterPosit;
            Debug.Log(Monster.transform.position);
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
        }
    }
}
