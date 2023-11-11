using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animation anim;
    SpriteRenderer spriteRenderer;
    Collider2D coll;

    public float jumpPower;
    public float health;
    public bool isLive;

    public bool isGround = true;

    private void Start()
    {
        isLive = true;
    }

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position + Vector2.down * 0.45f, Vector3.down, 1, LayerMask.GetMask("Platform"));

        // ��� ����
        if (Input.GetButtonDown("Jump") && isGround)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGround = false;
            coll.isTrigger = true;
        }

        // �Ϲ� ���� (���� : Platform�ϰ�, ���� ���������)
        if(Input.GetKeyDown(KeyCode.DownArrow) && isGround && rayHit.collider.CompareTag("Platform"))
        {
            isGround = false;
            coll.isTrigger = true;
            rayHit.collider.gameObject.GetComponent<Platform>().TemporaryDisable();
        }
        // �������϶� DownRay ����
        if (rigid.velocity.y < 0)
            GroundCheck(rayHit);
    }

    private void GroundCheck(RaycastHit2D rayHit)
    {
        if (rayHit.collider != null)
        {
            // ���� ��ȸ �ֱ�
            if (rayHit.distance < 0.05f)
            {
                isGround = true;
                coll.isTrigger = false;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLive)
        {
            if (collision.CompareTag("MonsterBullet"))
            {
                health -= collision.GetComponent<MonsterBullet>().damage;
                Debug.Log("�÷��̾� ü��: " + health);

                if (health < 1)
                {
                    isLive = false;
                    Debug.Log("�÷��̾� ���!! XOXO"); // ����
                }
            }
        }
    }

}