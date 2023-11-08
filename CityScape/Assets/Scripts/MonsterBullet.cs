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
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // �ҷ� ���������� �̵�

        if (transform.position.x < -16) // �Ѿ��� ȭ�� ������ ��� ��
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
        else return;
    }
}
