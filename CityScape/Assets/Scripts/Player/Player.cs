using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 싱글톤
    public static Player instance { get; set; }

    // 플레이어 스탯
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    Collider2D coll;

    public int playerHp; // 플레이어의 최대 hp
    public int playerCurrentHp; // 플레이어의 인게임상에서의 hp를 나타내기위한 변수
    public float playerOffence;
    public bool isLive;

    // 코인 관련
    public int totalMoney;
    public int currentMoney;

    // 점프 관련
    public bool isGround = true;
    public float jumpPower;

    // 구르기 관련
    public bool canRoll;
    public bool isRolling;
    public float rollingTime;
    private void Awake()
    {
        /* 싱글톤 적용 */
        if (instance == null)
            instance = this;

        coll = GetComponent<Collider2D>();
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    private void Start()
    {
        GameManager.instance.player = this;
        isLive = true;
        isRolling = false;
        playerHp = GameManager.instance.upgradeData.hpLevel + 2;
        playerOffence = GameManager.instance.upgradeData.offenceLevel * 1;
        playerCurrentHp = playerHp;

        anim.SetBool("isRolling", false);
    }

    public void JumpUp()
    {
        if (isGround)
        {
            GameManager.Sound.Play(Define.SFX.Char_jump_1128);
            isGround = false;
            coll.isTrigger = true;

            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    private void Update()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position +
            Vector2.down * 0.45f, Vector3.down, 1, LayerMask.GetMask("Platform"));

        // 상방 점프
        if (Input.GetButton("Jump"))
        {
            JumpUp();
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
            if (rayHit.distance < 0.1f)
            {
                isGround = true;
                coll.isTrigger = false;
            }
        }
    }

    public void getDamage(int damage)
    {
        if (isRolling) return;
        GameManager.Sound.Play(Define.SFX.Char_hit_1128);

        playerCurrentHp -= damage;
        ScoreManager.Score -= 1000; //피격 시 데미지
        Debug.Log($"플레이어 체력 : {playerCurrentHp}");
        if (playerCurrentHp <= 0)
        {
            isLive = false;
            GameManager.Sound.Play(Define.SFX.Char_death_1128);
            Debug.Log("플레이어 사망");
            gameObject.SetActive(false);
        }
    }

    // 구르기 : 구르기 시간동안 플레이어의 체력이 감소하지 않음
    IEnumerator Rolling()
    {
        // 구르기 상태 ON
        isRolling = true;
        Debug.Log("=> 구르기 ON, 현재 체력 : " + playerCurrentHp);
        anim.SetBool("isRolling", true);
        GameManager.Sound.Play(Define.SFX.Chac_roll_1128);

        // ~ 구르기 지속 시간 ~
        yield return new WaitForSeconds(rollingTime);

        // 구르기 상태 OFF
        isRolling = false;
        Debug.Log("=> 구르기 OFF, 현재 체력 : " + playerCurrentHp);
        anim.SetBool("isRolling", false);
        yield break;
    }

    public void OnCollisionEnter2D(Collision2D other){ //충돌 감지 => 
        if(other.gameObject.tag == "Coin5"){ //5코인 획득
            Debug.Log("5코인 획득");
            ScoreManager.Score += 20;
            GetMoney.getMoney += 5;
        }
        else if(other.gameObject.tag == "Coin10"){ //10코인 획득
            Debug.Log("10코인 획득");
            ScoreManager.Score += 20;
            GetMoney.getMoney += 10;
        }
    }
}

