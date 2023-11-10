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
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }
    }
}
