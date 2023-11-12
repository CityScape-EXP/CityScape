using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(i < 2)
        {
            Debug.Log("Enemy »ý¼º!");
            var enemy = PoolManager.GetObject(2);
            enemy.transform.position = transform.position + Vector3.zero;
            i++;
        }
    }
}
