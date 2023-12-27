using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    // 플레이어 생존시 총알 스폰 시작
    // 구르기시 총알 스폰 중지

    /* 총알 관련 */
    public float WaitForFirstSpawn;
    public float BulletSpawnTime;
    public Vector3 FixPos;

    /* 플레이어 관련 */
    private GameObject Player;
    public bool isPlayerLive;
    public bool isPlayerRolling;

    /* 초기화 */
    private void Awake()
    {
        Player = gameObject;
    }

    /* 플레이어가 생존 상태로 시작하고, 총알 생성 루틴을 실행함 */
    private void Start()
    {
        isPlayerLive = true;
        isPlayerRolling = false;

        StartCoroutine(SpawnRoutine());

        // 레벨에 따른 총알 쿨타임 조정
        int asLevel = GameManager.instance.upgradeData.asLevel;
        BulletSpawnTime = BulletSpawnTime / (1.00f + (asLevel - 1) * 0.25f);
    }

    /* 총알 생성 루틴 함수 */
    IEnumerator SpawnRoutine()
    {
        // 발사 시작 전 잠시 대기
        yield return new WaitForSeconds(WaitForFirstSpawn);

        while (true)
        {
            if (!isPlayerRolling) // 플레이어가 구르기 중이 아닌 경우에만 실행
            {
                // 풀 가져오기, 사운드
                GameObject obj = PoolManager.GetObject(0);
                GameManager.Sound.Play(Define.SFX.Char_gunfire_1);

                // 총알 발사 위치 세부 조정
                obj.transform.position = Player.transform.position + FixPos;

                // 총알 스폰 시간만큼 대기
                yield return new WaitForSeconds(BulletSpawnTime);
            }
            else
            {
                yield return null; // 플레이어가 구르기 중이면 한 프레임 대기 후 다시 확인
            }
        }

    }

    /* 플레이어 사망, 구르기 여부 체크 */
    private void Update()
    {
        if (Player != null)
        {
            Player PlayerObj = Player.GetComponent<Player>();

            if (PlayerObj != null)
            {
                isPlayerLive = PlayerObj.isLive;
                isPlayerRolling = PlayerObj.isRolling;
            }
        }
    }
}
