using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float time;

    // private float gameTime; ���� �ڵ�� �ּ� ó��


    void Start()
    {
        //  if (time > 3f) // 3�ʿ� ���� �����ϴ� �ڵ� ���׳��� �ּ�ó��
        //  {
        onStage();
        //  }
    }

    void Update()
    {
        time += Time.deltaTime;
        // gameTime = GetComponent<GameManager>().getTime();
        // if (gameTime > 4f) onStage(0);
    }

    void onStage()
    {

        GameManager.instance.pool.Get(0);
    }

}
