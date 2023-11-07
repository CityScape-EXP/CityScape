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

    public bool isMonsterLive;

    private void Start()
    {
        isMonsterLive = true; // Monster가 생존 상태로 시작
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() // 총알 생성 루틴
    {
        yield return new WaitForSeconds(BulletWaitTime);

        while (isMonsterLive)
        { // 몬스터 생존 경우만 총알 생성
            GameObject MonsterBullet = GameManager.instance.pool.Get(2);
            MonsterBullet.transform.position = Monster.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime 초 만큼 대기 후 실행
        }
    }

    private void Update() // 몬스터 사망 여부 체크
    {
        if (Monster != null){
            Monster MonsterObj = Monster.GetComponent<Monster>();
            if (MonsterObj != null){
                isMonsterLive = MonsterObj.isLive; // Monster 오브젝트의 isLive 변수 가져오기
                Debug.Log("MonsterObj.isLive : " + MonsterObj.isLive);
            }
        }
    }
}
