using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float id;
    public float prefabID;

    public float speed;
    public float health;
    public float power;
    public bool isLive; // ���� Instantiate�� isLive = true

    [SerializeField] float StartAppearTime;
    [SerializeField] float StartAppearSpeed;
    [SerializeField] float TimeLimit; // ���� óġ �ð� ����

    Rigidbody2D rigid;
    CapsuleCollider2D coll;

    private void Start()
    {
        isLive = true;
        coll = GetComponent<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(StartAppearSec(StartAppearTime)); // �� �ۿ��� ������ �����ϴ� �ð�
        StartCoroutine(DissaperaSec(TimeLimit)); // TimeLimit �ѱ�� �����
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLive)
        {
            if (collision.CompareTag("PlayerBullet"))
            {
                health -= collision.GetComponent<PlayerBullet>().damage;
                Debug.Log(id + "�� ���� ü��: " + health);

                if (health < 1)
                {
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
