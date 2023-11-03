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

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // 불렛 오른쪽으로 이동

        /*
        if (transform.position.x > 12f){
            GameManager.instance.pool.Clear(0);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌 감지
    {
        if (collision.CompareTag("Monster")){
            Debug.Log("충돌");
            gameObject.SetActive(false);
        }
    }


}
