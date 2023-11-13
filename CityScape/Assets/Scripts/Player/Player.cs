using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �̱���
    public static Player instance = null;

    // �÷��̾� ����
    Rigidbody2D rigid;
    Animation anim;
    SpriteRenderer spriteRenderer;
    Collider2D coll;

    public float jumpPower;
    private int playerHp;
    public float playerOffence;
    public bool isLive;

    public bool isGround = true;

    // ������ ����
    public bool canRoll;
    public float rollingTime;

    private void Start()
    {
        isLive = true;
        playerHp = GameManager.instance.upgradeData.hpLevel + 2;
        playerOffence = GameManager.instance.upgradeData.offenceLevel * 1;
    }

    private void Awake()
    {
        /* �̱��� ���� */
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �ı� ����
        }
        else
        {
            Destroy(gameObject); // �ı�
        }

        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animation>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position +
            Vector2.down * 0.45f, Vector3.down, 1, LayerMask.GetMask("Platform"));

        // ��� ����
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isGround = false;
            coll.isTrigger = true;

            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        // �Ϲ� ���� (���� : Platform�ϰ�, ���� ���������)
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGround && rayHit.collider.CompareTag("Platform"))
        {
            isGround = false;
            coll.isTrigger = true;

            rayHit.collider.gameObject.GetComponent<Platform>().TemporaryDisable();
        }

        // �������϶� DownRay ����
        if (rigid.velocity.y < 0)
            GroundCheck(rayHit);

        // ������
        if (Input.GetButtonDown("Lshift") && isGround)
        {
            canRoll = true;
            StartCoroutine(Rolling());
        }
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

    public void getDamage(int damage)
    {
        playerHp -= damage;
        Debug.Log($"�÷��̾� ü�� : {playerHp}");
        if (playerHp <= 0)
        {
            isLive = false;
            Debug.Log("�÷��̾� ���");
            gameObject.SetActive(false);
        }
    }

    // ������
    // : ������ �ð����� �÷��̾�� ���� �Ѿ��� �浹�� ��Ȱ��ȭ��, �Ǵ� �������� �������� ����
    IEnumerator Rolling()
    {
        // �÷��̾� �浹 ���� ��Ȱ��ȭ
        yield return new WaitForSeconds(rollingTime);
    }
}
