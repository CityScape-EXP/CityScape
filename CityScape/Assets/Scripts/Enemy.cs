using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float power;
    public bool isLive = true; // ���� Instantiate�� isLive = true

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

    private void FixedUpdate() // ���������� �÷����� ���� �����̵��� �����߽��ϴ�
    {
        if (isLive) MovePosit();
    }

    void MovePosit()
    {
        rigid.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        rigid.velocity = Vector2.zero; // ���� �ӵ��� �̵��� ������ ���� �ʵ��� ��
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
