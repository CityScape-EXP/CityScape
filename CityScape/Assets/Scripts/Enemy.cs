using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float power;
    public bool isLive; // ���� Instantiate�� isLive = true

    [SerializeField] float StartAppearTime;
    [SerializeField] float StartAppearSpeed;
    [SerializeField] float TimeLimit;

    public float id;
    public float prefabID;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    CapsuleCollider2D coll;

    private void Start()
    {
        isLive = true;
        StartCoroutine(StartAppearSec(StartAppearTime)); // �� �ۿ��� ������ �����ϴ� �ð�
        StartCoroutine(DissaperaSec(TimeLimit)); // TimeLimit �ѱ�� �����

    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLive){
            if (collision.collider.CompareTag("Bullet")){
                health -= collision.collider.GetComponent<Bullet>().damage;
                Debug.Log(health);

                if (health < 0){
                    isLive = false;
                    Debug.Log("���� ���!! XOXO"); // ����
                    gameObject.SetActive(false); // ��Ȱ��ȭ
                }
            }
        }
    }

    IEnumerator StartAppearSec(float seconds)
    {
        float endTime = Time.time + seconds;

        while (Time.time < endTime)
        {
            rigid.MovePosition(transform.position + Vector3.left * StartAppearSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator DissaperaSec(float seconds)
    {
        float endTime = Time.time + seconds;

        while (Time.time < endTime)
        {
            rigid.MovePosition(transform.position + Vector3.left * StartAppearSpeed * Time.deltaTime);
            yield return null;
        }
    }


}
