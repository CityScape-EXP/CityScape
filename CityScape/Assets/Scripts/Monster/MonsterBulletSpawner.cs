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

    /* 초기화 */
    private void Awake()
    {
        MyMonster = gameObject;
    }

    /* 몬스터가 생존 상태로 시작하고, 총알 생성 루틴을 실행함 */
    private void Start()
    {
        isMonsterLive = true;

        StartCoroutine(SpawnRoutine());
    }

    /* 총알 생성 루틴 함수 */
    IEnumerator SpawnRoutine()
    {
        while (isMonsterLive) // 몬스터 생존 경우만 총알 생성
        {
            GameObject MonsterBullet = PoolManager.GetObject(1);
            // Pool의 1번째 프리펩(총알)을 가져옴. 나중에 해당 부분 수정 가능성 있음
            MonsterBullet.transform.position = MyMonster.transform.position + FixPos; // 총알 생성 위치 조정

            yield return new WaitForSeconds(BulletSpawnTime); // SpawnColltime초 만큼 대기 후 실행
        }
    }

    /* 몬스터 사망 여부 체크 */
    private void Update() 
    {
        if (MyMonster != null){
            Monster MonsterObj = MyMonster.GetComponent<Monster>();

            if (MonsterObj != null){
                isMonsterLive = MonsterObj.isLive; // Monster 오브젝트의 isLive 변수 가져오기
            }
        }
    }
}
