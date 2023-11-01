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

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
    }

    private void FixedUpdate() // ���������� �÷����� ���� �����̵��� �����߽��ϴ�
    {
        if (!isLive) return; // isLive = true ��쿡�� �̵�

        rigid.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
        rigid.velocity = Vector2.zero; // ���� �ӵ��� �̵��� ������ ���� �ʵ��� ��
    }

}
