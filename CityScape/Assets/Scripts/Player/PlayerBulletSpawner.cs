using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    public GameObject Player;
    public int id;

    public Vector3 FixPos; // 총알 생성 위치 조정

    public float BulletSpawnTime;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject obj = PoolManager.GetObject(0); // 이 부분 인덱스 나즁에 id로 수정
            obj.transform.position = Player.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime 초 만큼 대기 후 실행
            
        }
    }
}
