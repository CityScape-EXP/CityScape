using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    private Vector3 targetPosition; // �̵��� ��ǥ ��ġ

    private void Start()
    {
        SetRandomPosition(); // �ʱ� ���� ��ġ ����
    }

    private void SetRandomPosition()
    {
        float randomX = Random.Range(2f, 10f); // X ��ǥ ���� ����
        //float randomY = Random.Range(-5f, 5f); // Y ��ǥ ���� ����
        targetPosition = new Vector3(randomX, transform.position.y, transform.position.z); // ���� ��ġ ����
    }

    private void FixedUpdate()
    {
        MoveToTargetPosition(); // ��ǥ ��ġ�� �̵�
    }

    private void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // ���� ��ġ���� ��ǥ ��ġ�� ���� �ӵ��� �̵�

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {   // ���� ��ġ�� ��ǥ ��ġ�� �������� ��
            SetRandomPosition(); // ���ο� ���� ��ġ ����
        }
    }
}
