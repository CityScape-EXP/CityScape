using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int id;
    public float prefabID;

    public float health;
    public float power;
    public bool isLive; 

    public GameObject coin5Prefab;
    public GameObject coin10Prefab;

    [SerializeField]
    Define.MonsterType monsterType;

    Rigidbody2D rigid;
    CapsuleCollider2D coll;

    private void Start()
    {
        health = health * (1 + (GameManager.instance.stageNum - 1) * 0.5f) ;
        isLive = true;
        coll = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void getDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            isLive = false;

            switch(monsterType)
            {
                case Define.MonsterType.NorEnemy: // 일반몬스터 처치
                    ScoreManager.ObjectScore += 200;
                    GameManager.Sound.Play(Define.SFX.Enemy_death_1);
                    break;
                case Define.MonsterType.FlyEnemy: // 날몹 처치
                    ScoreManager.ObjectScore += 500;
                    GameManager.Sound.Play(Define.SFX.Enemy_death_1);
                    break;
                case Define.MonsterType.ReinEnemy:
                    ScoreManager.ObjectScore += 350;
                    break;
            }
            this.DropCoin(); //오브젝트가 사라지기 전 매서드 달아주기
            this.gameObject.SetActive(false);
        }
        else
        {
            GameManager.Sound.Play(Define.SFX.Enemy_hit_1);
        }
    }

    public void DropCoin(){
        float randomValue = Random.value; //0에서 1사이의 랜덤값

        if (randomValue <= 0.7f){// 70%의 확률로 5코인생성
            GameObject coin5 = Instantiate(coin5Prefab, transform.position, Quaternion.identity);
            Destroy(coin5, 6.0f); //6초 후 파괴
        }
        else{ // 10코인 생성
            GameObject coin10 = Instantiate(coin10Prefab, transform.position, Quaternion.identity);
            Destroy(coin10, 6.0f);
        }
    }
}