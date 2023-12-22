using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int id;
    public float prefabID;

    public float health;
    public float power;
    public bool isLive; // ���� Instantiate�� isLive = true

    public GameObject coin5Prefab;
    public GameObject coin10Prefab;

    Rigidbody2D rigid;
    CapsuleCollider2D coll;

    private void Start()
    {
        isLive = true;
        coll = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void getDamage(float damage)
    {
        health -= damage;
        Debug.Log($"���� ü�� : {health}");
        if (health <= 0)
        {
            isLive = false;

            switch(id)
            {
                case 0: // 일반몬스터 처치
                    ScoreManager.Score += 100;
                    break;
                case 1: // 날몹 처치
                    ScoreManager.Score += 200;
                    break;
                case 2:
                    ScoreManager.Score += 150;
                    break;
            }
            this.DropCoin(); //오브젝트가 사라지기 전 매서드 달아주기
            this.gameObject.SetActive(false);
        }
    }

    public void DropCoin(){
        float randomValue = Random.value; //0에서 1사이의 랜덤값

        if (randomValue <= 0.7f){// 70%의 확률로 5코인생성
        Instantiate(coin5Prefab, transform.position, Quaternion.identity);
        }
        else{ // 10코인 생성
        Instantiate(coin10Prefab, transform.position, Quaternion.identity);
        }
    }
}