using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMove : MonoBehaviour
{
    private float moveSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);    
    }
}
