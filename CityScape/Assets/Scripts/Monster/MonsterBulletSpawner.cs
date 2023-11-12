using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletSpawner : MonoBehaviour
{
    GameObject MyMonster;

    public Vector3 FixPos; // 총알 생성 위치 조정

    public float WaitForApeear;
    public float SpawnColltime;

    public bool isMonsterLive;


    private void Start()
    {
        isMonsterLive = true; // Monster가 생존 상태로 시작
        MyMonster = PoolManager.GetObject(2);
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() // 총알 생성 루틴
    {
        yield return new WaitForSeconds(WaitForApeear);

        while (isMonsterLive) // 몬스터 생존 경우만 총알 생성
        { 
            GameObject MonsterBullet = PoolManager.GetObject(1);
            MonsterBullet.transform.position = MyMonster.transform.position + FixPos;
            yield return new WaitForSeconds(SpawnColltime); // BulletSpawnTime 초 만큼 대기 후 실행
        }
    }

    private void Update() // 몬스터 사망 여부 체크
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
}
