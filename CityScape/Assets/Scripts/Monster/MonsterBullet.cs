using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    //BoxCollider2D coll;
    public float moveSpeed;
    public int damage;
    public float id;
    public float prefabID;
    public int shooterID;
    public Vector3 playerDir;

    void Start()
    { 
        Vector3 playerPos = GameManager.instance.player.transform.position;
        playerDir = (playerPos - transform.position).normalized;
        //coll = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
    }

    private void FixedUpdate()
    {
        if (shooterID == 0)
            normalEnemyShoot();
        else if(shooterID == 1)
            flyingEnemyShoot(playerDir);
    }

    private void OnTriggerEnter2D(Collider2D collision) // �浹 ����
    {
        if (collision.CompareTag("Player"))
        {
            PoolManager.ReturnObject(this.gameObject, 1);
            collision.GetComponent<Player>().getDamage(damage);
        }
        else return;
    }

    private void normalEnemyShoot()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime); // �ҷ� ���������� �̵�

        if (transform.position.x < -16) // �Ѿ��� ȭ�� ������ ��� ��
        {
            PoolManager.ReturnObject(this.gameObject, 1);
        }
    }

    private void flyingEnemyShoot(Vector3 dir)
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        if(transform.position.x < -16)
        {
            PoolManager.ReturnObject(this.gameObject, 1);
        }
    }
}
