using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    
    public GameObject MyMonster;

    // Start is called before the first frame update
    
    void Start()
    {
        MyMonster = PoolManager.GetObject(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
