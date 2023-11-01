using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    BoxCollider2D coll;
    public float moveSpeed;
    public float startPos;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
