using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletSpawner : MonoBehaviour
{
    /* 총알 관련 */
    public float BulletSpawnTime;
    public Vector3 FixPos;

    /* 몬스터 관련 */
    GameObject MyMonster;
    public bool isMonsterLive;
    private bool isMonsterExit;

    /* 초기화 */
    private void Awake()
    {
        MyMonster = gameObject;
    }

    /* 몬스터가 생존 상태로 시작하고 코루틴을 시작함 */
    private void Start()
    {
        isMonsterLive = true;
        isMonsterExit = false;

        StartCoroutine(CheckMonsterPosit());
    }

    /* 몬스터 사망 여부 체크 */
    private void Update()
    {
        if (MyMonster != null)
        {
            Monster MonsterObj = MyMonster.GetComponent<Monster>();

            if (MonsterObj != null)
            {
                isMonsterLive = MonsterObj.isLive; // Monster 오브젝트의 isLive 변수 가져오기
            }
        }
    }

    /* 몬스터가 화면 내에 등장하면 총알 생성 루틴 시작 */
    IEnumerator CheckMonsterPosit()
    {
        while (isMonsterLive)
        {
            if (gameObject.transform.position.x < 12)
            {
                StartCoroutine(SpawnRoutine());
                yield break;
            }

            yield return null;
        }
    }

    /* 총알 생성 루틴 함수 */
    IEnumerator SpawnRoutine()
    {
        StopCoroutine(CheckMonsterPosit());
        while (isMonsterLive && !isMonsterExit) // 몬스터 생존 & 퇴장중이 아닐 경우만 총알 생성
        {
            // PoolManager에서 Bullet 가져 옴 ( 1 : 몬스터의 Bullet )
            GameObject Bullet = PoolManager.GetObject(1);
            var MonsterBullet = Bullet.GetComponent<MonsterBullet>();
            // MonsterBullet 각종 옵션 세팅
            MonsterBullet.shooterID = MyMonster.GetComponent<Monster>().id;
            MonsterBullet.playerDir = (GameManager.instance.player.transform.position
                - gameObject.transform.position).normalized;
            // Pool의 1번째 프리펩(총알)을 가져옴. 나중에 해당 부분 수정 가능성 있음
            MonsterBullet.transform.position = MyMonster.transform.position + FixPos; // 총알 생성 위치 조정
            GameManager.Sound.Play(Define.SFX.Enemy_gunfire_1);

            yield return new WaitForSeconds(BulletSpawnTime); // SpawnColltime초 만큼 대기 후 실행
        }
    }

    public void MonsterExit() { isMonsterExit = true; }

}
