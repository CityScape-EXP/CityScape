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
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject Obj = GameManager.instance.BulletPool.Get(0);
            Obj.transform.position = Player.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
            
        }
    }
}