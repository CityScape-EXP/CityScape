using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    public bool isAppear;
    public float minX;
    public float maxX;

    private float targetX; // 이동할 목표 위치

    private void Start()
    {
        targetX = 10;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < 11)
            MoveToTargetPosition(); // 목표 위치로 이동
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
}
