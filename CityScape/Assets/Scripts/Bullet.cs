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

    private void FixedUpdate() // �Ѿ� ��ġ ����
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        if (collision.CompareTag("Monster")){
            Destroy(this); // �ش� �ڵ�� ��ũ��Ʈ�� �����մϴ�. ��, ������Ʈ Ǯ������ ��ȯ �� ��ü �� �����Դϴ�.
        }
    }
}
