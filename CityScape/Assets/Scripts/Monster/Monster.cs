using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float id;
    public float prefabID;

    public float health;
    public float power;
    public bool isLive; // ���� Instantiate�� isLive = true

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
            if (collision.CompareTag("PlayerBullet")) // �Ѿ� �浹�� ü�� ����
            {
                health -= collision.GetComponent<PlayerBullet>().damage;
                Debug.Log(id + "�� ���� ü��: " + health);

                if (health < 1)
                {
                    isLive = false;
                    Debug.Log("���� ���!! XOXO"); // ����
                    gameObject.SetActive(false); // ��Ȱ��ȭ
                }
            }
        }
    }
}
