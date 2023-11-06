using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // 인스펙터에서 초기화
    public GameObject Monster;
    public int id;

    public Vector3 FixPos; // 총알 생성 위치 조정

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
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime 초 만큼 대기 후 실행
        }
    }

    public void StopBullet()
    {
        StopCoroutine(SpawnRoutine()); // 몬스터가 죽으면 총알 생성 루틴을 중지
    }
}
