using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    /* 총알 관련 */
    public float WaitForFirstSpawn;
    public float BulletSpawnTime;
    public Vector3 FixPos;

    /* 플레이어 관련 */
    private GameObject Player;
    public bool isPlayerLive;

    /* 초기화 */
    private void Awake()
    {
        Player = gameObject;
    }

    /* 플레이어가 생존 상태로 시작하고, 총알 생성 루틴을 실행함 */
    private void Start()
    {
        isPlayerLive = true;

        StartCoroutine(SpawnRoutine());

        // 레벨에 따른 총알 쿨타임 조정
        int asLevel = GameManager.instance.upgradeData.asLevel;
        BulletSpawnTime = BulletSpawnTime / (1.00f + (asLevel - 1) * 0.25f);
    }

    /* 총알 생성 루틴 함수 */
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(WaitForFirstSpawn);
        // WaitForFirstSpawn 초 이후 총알 스폰 시작

        while (true)
        {
            GameObject obj = PoolManager.GetObject(0);
            // Pool의 0번째 프리펩(총알)을 가져옴. 나중에 해당 부분 수정 가능성 있음
            obj.transform.position = Player.transform.position + FixPos;
            
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime 초 만큼 대기 후 실행
        }
    }

    /* 플레이어 사망 여부 체크 */
    private void Update()
    {
        if (Player != null)
        {
            Player PlayerObj = Player.GetComponent<Player>();

            if (PlayerObj != null)
            {
                isPlayerLive = PlayerObj.isLive; // Monster 오브젝트의 isLive 변수 가져오기
            }
        }
    }
}
