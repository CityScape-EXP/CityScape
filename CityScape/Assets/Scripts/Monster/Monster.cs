using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int id;
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

    public void getDamage(float damage)
    {
        health -= damage;
        Debug.Log($"몬스터 체력 : {health}");
        if (health <= 0)
        {
            isLive = false;
            Debug.Log("몬스터 사망");
            this.gameObject.SetActive(false);
        }
    }
}