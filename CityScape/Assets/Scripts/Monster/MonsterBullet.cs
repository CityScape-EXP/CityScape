using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    //BoxCollider2D coll;
    public float moveSpeed;
    public float damage;
    public float id;
    public float prefabID;

    void Start()
    {
        //coll = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // 불렛 오른쪽으로 이동

        if (transform.position.x < -16) // 총알이 화면 밖으로 벗어날 시
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌 감지
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else return;
    }
}
