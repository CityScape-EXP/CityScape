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
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // �ҷ� ���������� �̵�

        /*
        if (transform.position.x > 12f){
            GameManager.instance.pool.Clear(0);
        }
        */
    }

    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        if (collision.CompareTag("Monster")){
            Debug.Log("�浹");
            gameObject.SetActive(false);
        }
    }


}
