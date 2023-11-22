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
    public int playerHp;
    public int playerCurrentHp;
    public float playerOffence;
    public bool isLive;

    public bool isGround = true;

    // 구르기 관련
    public bool canRoll;
    public bool isRolling;
    public float rollingTime;

    private void Start()
    {
        GameManager.instance.player = this;
        isLive = true;
        isRolling = false;
        playerHp = GameManager.instance.upgradeData.hpLevel + 2;
        playerOffence = GameManager.instance.upgradeData.offenceLevel * 1;
        playerCurrentHp = playerHp;
    }

    private void Awake()
    {
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
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

        // 구르기 (조건: 땅에 있을 것, 구르고 있지 않을 것)
        if (Input.GetButtonDown("Lshift") && isGround && !isRolling)
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
        playerCurrentHp -= damage;
        Debug.Log($"플레이어 체력 : {playerCurrentHp}");
        if (playerCurrentHp <= 0)
        {
            isLive = false;
            Debug.Log("플레이어 사망");
            gameObject.SetActive(false);
        }
    }

    // 구르기 : 구르기 시간동안 플레이어와 몬스터 총알의 충돌을 비활성화함, 또는 데미지가 감소하지 않음
    IEnumerator Rolling()
    {
        isRolling = true;
        Debug.Log(" => 구르기 <= ");
        Debug.Log("isRolling : " + isRolling);

        // 총알 충돌 무시

        int playerLayer = gameObject.layer;
        int bulleyLayer = LayerMask.NameToLayer("Bullet");
        Physics2D.IgnoreLayerCollision(bulleyLayer, playerLayer, false);

        // 구르기 지속 시간
        yield return new WaitForSeconds(rollingTime);

        // 총알 충돌 무시 해제
        Physics2D.IgnoreLayerCollision(bulleyLayer, playerLayer, true);

        isRolling = false;
        Debug.Log(" => 구르기 종료 <= ");
        Debug.Log("isRolling : " + isRolling);
        yield break;
    }
}
