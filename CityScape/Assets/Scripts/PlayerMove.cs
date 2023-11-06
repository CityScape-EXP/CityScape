using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void fixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal"); //h: 오른쪽(1), 왼쪽(-1), 가운데(0)

        rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);
    }
}
