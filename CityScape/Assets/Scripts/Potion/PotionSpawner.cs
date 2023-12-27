using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public float WaitForFirstSpawn;
    public float PotionSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 포션 생성 루틴 함수 */
    IEnumerator SpawnRoutine()
    {
        // 포션 생성 전 잠시 대기
        yield return new WaitForSeconds(WaitForFirstSpawn);

        while (true)
        {
            // 풀 가져오기
            GameObject PotionObj = PoolManager.GetObject(7);

            // 총알 발사 위치 세부 조정
            PotionObj.transform.position = gameObject.transform.position;

            // 총알 스폰 시간만큼 대기
            yield return new WaitForSeconds(PotionSpawnTime);
            
        }

    }
}
