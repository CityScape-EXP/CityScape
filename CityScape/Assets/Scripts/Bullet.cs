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
        transform.position = GameManager.instance.player.transform.position + FixPos; // �ҷ� ���� ��ġ: �÷��̾� ��ġ
    }

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // �ҷ� ���������� �̵�
    }

    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        if (collision.CompareTag("Monster")){ 
            GameManager.instance.pool.Clear(0); // ������ ��Ȱ��ȭ
        }
    }
}
