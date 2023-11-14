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
            Debug.Log("���� ���");
            this.gameObject.SetActive(false);
        }
    }
}