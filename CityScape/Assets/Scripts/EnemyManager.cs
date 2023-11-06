using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    void Start()
    {
        onStage();
    }

    void Update()
    {
    }

    void onStage()
    {
        GameManager.instance.pool.Get(1);
    }

}
