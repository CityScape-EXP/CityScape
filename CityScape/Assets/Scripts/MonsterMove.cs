using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    private Vector3 targetPosition; // 이동할 목표 위치

    private void Start()
    {
        SetRandomPosition(); // 초기 랜덤 위치 설정
    }

    private void SetRandomPosition()
    {
        float randomX = Random.Range(2f, 10f); // X 좌표 범위 설정
        //float randomY = Random.Range(-5f, 5f); // Y 좌표 범위 설정
        targetPosition = new Vector3(randomX, transform.position.y, transform.position.z); // 랜덤 위치 설정
    }

    private void FixedUpdate()
    {
        MoveToTargetPosition(); // 목표 위치로 이동
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // 현재 위치에서 목표 위치로 일정 속도로 이동

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {   // 현재 위치가 목표 위치에 도달했을 때
            SetRandomPosition(); // 새로운 랜덤 위치 설정
        }
    }
}
