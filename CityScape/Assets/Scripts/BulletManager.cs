using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // 인스펙터에서 초기화
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
            GameObject Obj = GameManager.instance.pool.Get(0);
            Obj.transform.position = Player.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime 초 만큼 대기 후 실행
            
        }
    }
}
