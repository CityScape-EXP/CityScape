using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //BoxCollider2D coll;
    public float moveSpeed;
    private float damage;
    public float id;
    public float prefabID;

    void Start()
    {
        int offenceLevel = GameManager.instance.upgradeData.offenceLevel;
        damage = 1f + 1f * (offenceLevel - 1);
        //coll = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // �ҷ� ���������� �̵�

        if (transform.position.x > 12) // �Ѿ��� ȭ�� ������ ��� ��
        {
            PoolManager.ReturnObject(this.gameObject, 0);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        if (collision.CompareTag("Monster")){
            collision.gameObject.GetComponent<Monster>().getDamage(damage);
            PoolManager.ReturnObject(this.gameObject, 0);
        }
    }
}
