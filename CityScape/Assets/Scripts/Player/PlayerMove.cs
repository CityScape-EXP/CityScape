using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    public float speed;

    public float minX;
    public float maxX;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // 플레이어 이동 위치 제한 : 화면 내
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        // 오른쪽 경계를 넘어간 경우
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }

        // 경계 내부에 있는 경우
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if (horizontalInput < 0) // 뒤로 이동할 때 -> 좀 더 빠르게
            {
                transform.Translate(Vector3.right * Time.deltaTime * 5.8f * horizontalInput);
            }
            else
            {
                transform.Translate(Vector3.right * Time.deltaTime * 4.2f * horizontalInput);
            }
        }
    }
}
