using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    public GameObject Player;
    public int id;

    public Vector3 FixPos; // �Ѿ� ���� ��ġ ����

    public float BulletSpawnTime;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
        int asLevel = GameManager.instance.upgradeData.asLevel;
        BulletSpawnTime = BulletSpawnTime / (1.00f + (asLevel - 1) * 0.25f);
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject obj = PoolManager.GetObject(0);
            obj.transform.position = Player.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
            
        }
    }
}
