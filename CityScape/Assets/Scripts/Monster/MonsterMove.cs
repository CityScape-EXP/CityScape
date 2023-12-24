using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    public bool isAppear = false;
    public float minX;
    public float maxX;
    float appearTime = 0f; // 몬스터가 등장하고 흐른 시간
    private float targetX; // 이동할 목표 위치
    SpriteRenderer targetRenderer;

    private void Start()
    {
        if(targetRenderer == null)
            targetRenderer = GetComponent<SpriteRenderer>();
        targetX = 10;
    }

    private void FixedUpdate()
    {
        if(isAppear)
            appearTime += Time.deltaTime;
        if (appearTime > 7) // 생성 후 10초가 지나면 ? -> 퇴장 액션
        {
            this.GetComponent<MonsterBulletSpawner>().enabled = false; // 총알 생성 로직 멈춤
            targetRenderer.material.SetColor("_Color", new Color(255, 255, 255, 0.5f));
            transform.Translate(Vector3.left * Time.deltaTime * 10);
            if(transform.position.x < -15)
                Destroy(gameObject);
        }
        else if (transform.position.x < 11)
        {
            isAppear = true;
            MoveToTargetPosition(); // 목표 위치로 이동
        }
        else
            WaitForAppear();
    }

    private void MoveToTargetPosition()
    {
        // 현재 위치에서 목표 위치로 일정 속도로 이동
        Vector3 monsterDirection = new Vector3(targetX - transform.position.x, 0, 0).normalized;
        if (monsterDirection.x > 0)
            speed = 3;
        else
            speed = 7;
        transform.Translate(monsterDirection * Time.deltaTime * speed);

        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {   // 현재 위치가 목표 위치에 도달했을 때
            targetX = Random.Range(minX, maxX);
        }
    }
    
    private void WaitForAppear()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 5);
    }

    private void MonsterExit()
    {
        Destroy(this);
    }
}
