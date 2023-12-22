using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Collider2D coll;
    public float moveSpeed = 5f;
    
    private void Awake()
    {
        coll = GetComponent<Collider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
