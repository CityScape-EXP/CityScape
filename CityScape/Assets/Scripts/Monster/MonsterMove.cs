using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    public bool isAppear;
    public float minX;
    public float maxX;

    private float targetX; // �̵��� ��ǥ ��ġ

    private void Start()
    {
        targetX = 10;
    }

    private void FixedUpdate()
    {
        if (transform.position.x < 11)
            MoveToTargetPosition(); // ��ǥ ��ġ�� �̵�
        else
            WaitForAppear();
    }

    private void MoveToTargetPosition()
    {
        // ���� ��ġ���� ��ǥ ��ġ�� ���� �ӵ��� �̵�
        Vector3 monsterDirection = new Vector3(targetX - transform.position.x, 0, 0).normalized;
        if (monsterDirection.x > 0)
            speed = 3;
        else
            speed = 7;
        transform.Translate(monsterDirection * Time.deltaTime * speed);

        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {   // ���� ��ġ�� ��ǥ ��ġ�� �������� ��
            targetX = Random.Range(minX, maxX);
        }
    }
    
    private void WaitForAppear()
    {
        transform.Translate(Vector3.left * Time.deltaTime * 5);
    }
}
