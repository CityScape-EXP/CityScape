using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    BoxCollider2D coll;
    public float moveSpeed;
    public float damage;
    public float id;
    public float prefabID;

    [SerializeField] private Vector3 FixPos;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        transform.position = GameManager.instance.player.transform.position + FixPos; // 불렛 생성 위치: 플레이어 위치
    }

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // 불렛 오른쪽으로 이동
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌 감지
    {
        if (collision.CompareTag("Monster")){ 
            GameManager.instance.pool.Clear(0); // 프리펩 비활성화
        }
    }
}
