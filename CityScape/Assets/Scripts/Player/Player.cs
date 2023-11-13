using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 싱글톤
    public static Player instance = null;

    // 플레이어 스탯
    Rigidbody2D rigid;
    Animation anim;
    SpriteRenderer spriteRenderer;
    Collider2D coll;

    public float jumpPower;
    private int playerHp;
    public float playerOffence;
    public bool isLive;

    public bool isGround = true;

    // 구르기 관련
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
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 파괴 금지
        }
        else
        {
            Destroy(gameObject); // 파괴
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

        // 상방 점프
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isGround = false;
            coll.isTrigger = true;

            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }

        // 하방 점프 (조건 : Platform일것, 땅에 닿아있을것)
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGround && rayHit.collider.CompareTag("Platform"))
        {
            isGround = false;
            coll.isTrigger = true;

            rayHit.collider.gameObject.GetComponent<Platform>().TemporaryDisable();
        }

        // 낙하중일때 DownRay 실행
        if (rigid.velocity.y < 0)
            GroundCheck(rayHit);

        // 구르기
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
            // 점프 기회 주기
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
        Debug.Log($"플레이어 체력 : {playerHp}");
        if (playerHp <= 0)
        {
            isLive = false;
            Debug.Log("플레이어 사망");
            gameObject.SetActive(false);
        }
    }

    // 구르기
    // : 구르기 시간동안 플레이어와 몬스터 총알의 충돌을 비활성화함, 또는 데미지가 감소하지 않음
    IEnumerator Rolling()
    {
        // 플레이어 충돌 감지 비활성화
        yield return new WaitForSeconds(rollingTime);
    }
}
