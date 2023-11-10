using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float id;
    public float prefabID;

    public float health;
    public float power;
    public bool isLive; // 최초 Instantiate시 isLive = true

    Rigidbody2D rigid;
    CapsuleCollider2D coll;

    private void Start()
    {
        isLive = true;
        coll = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLive)
        {
            if (collision.CompareTag("PlayerBullet")) // 총알 충돌시 체력 감소
            {
                health -= collision.GetComponent<PlayerBullet>().damage;
                Debug.Log(id + "번 몬스터 체력: " + health);

                if (health < 1)
                {
                    isLive = false;
                    Debug.Log("몬스터 사망!! XOXO"); // 죽음
                    gameObject.SetActive(false); // 비활성화
                }
            }
        }
    }
}
