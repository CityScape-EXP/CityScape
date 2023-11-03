using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // �ν����Ϳ��� �ʱ�ȭ
    public GameObject Player;
    public int id;

    public Vector3 FixPos; // �Ѿ� ���� ��ġ ����

    public float BulletSpawnTime;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject Obj = GameManager.instance.pool.Get(0);
            Obj.transform.position = Player.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
            
        }
    }
}
