using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    CircleCollider2D coll;
    float moveSpeed = 5f;   // 배경 이동 속도와 동일
    private int potionHP = 1;    // 포션 회복량

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 배경 이동 속도와 동일하게 이동
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    // potionHP를 반환하는 함수
    public int getPotionHP()
    {
        return potionHP;
    }
}
