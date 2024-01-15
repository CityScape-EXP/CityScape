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
    private Animator anim;
    private int offenceLevel;

    void Awake()
    {
        offenceLevel = DataManager.MainGameData.reinforceLevel[(int)Define.Reinforcement.Power]; // 수정 요망
        anim = GetComponent<Animator>();
        damage = 1f + 0.5f * (offenceLevel - 1);
        
        //coll = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // 불렛 오른쪽으로 이동

        anim.SetInteger("Enhanced", offenceLevel);

        if (transform.position.x > 11) // 총알이 화면 밖으로 벗어날 시
        {
            PoolManager.ReturnObject(this.gameObject, 0);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // 충돌 감지
    {
        if (collision.CompareTag("Monster")){
            collision.gameObject.GetComponent<Monster>().getDamage(damage);
            PoolManager.ReturnObject(this.gameObject, 0);
        }
    }
}
