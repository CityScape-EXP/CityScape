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

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
    }

    private void FixedUpdate() // 총알 위치 변경
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌 감지
    {
        if (collision.CompareTag("Monster")){
            Destroy(this); // 해당 코드는 스크립트를 삭제합니다. 즉, 오브젝트 풀링으로 변환 시 교체 할 예정입니다.
        }
    }
}
