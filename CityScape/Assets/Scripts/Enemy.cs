using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float power;
    public bool isLive = true; // 최초 Instantiate시 isLive = true

    public int EnemyIndex;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    CapsuleCollider2D coll;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate() // 일차적으로 플랫폼과 같이 움직이도록 구현했습니다
    {
        if (isLive) MovePosit();
    }

    void MovePosit()
    {
        rigid.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        rigid.velocity = Vector2.zero; // 물리 속도가 이동에 영향을 주지 않도록 함
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLive)
        {
            if (collision.collider.CompareTag("Bullet"))
            {
                health -= collision.collider.GetComponent<Bullet>().damage;
            }
        }
    }


}
