using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    //BoxCollider2D coll;
    public float moveSpeed;
    private float damage;
    public float id;
    public float prefabID;
    private Animator anim;
    [SerializeField]
    private AnimatorController[] animatorController;
    private int offenceLevel;

    void Start()
    {
        offenceLevel = GameManager.instance.upgradeData.offenceLevel;
        gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController[offenceLevel - 1];
        damage = 1f + 1f * (offenceLevel - 1);
        anim = GetComponent<Animator>();
        //coll = GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController = animatorController[offenceLevel - 1];
    }

    private void FixedUpdate()
    { 
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // 불렛 오른쪽으로 이동

        if (transform.position.x > 12) // 총알이 화면 밖으로 벗어날 시
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
