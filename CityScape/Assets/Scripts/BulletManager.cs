using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // 인스펙터에서 초기화
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
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime 초 만큼 대기 후 실행
            Instantiate(Obj, transform.position, Quaternion.identity); // 오브젝트 생성
        }
    }
}
