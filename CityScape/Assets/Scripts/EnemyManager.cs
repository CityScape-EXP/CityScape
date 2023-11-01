using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] prefabs;
    float time;

    // private float gameTime; 에러 코드라서 주석 처리

    void Start()
    {
        //  if (time > 3f) // 3초에 몬스터 스폰하는 코드 버그나서 주석처리
        //  {
        onStage(0); // 프리펩 0번째 몬스터 스폰
        //  }
    }

    void Update()
    {
        time += Time.deltaTime;
        // gameTime = GetComponent<GameManager>().getTime();
        // if (gameTime > 4f) onStage(0);
    }

    private void FixedUpdate()
    {
        
    }

    public void onStage(int index) // 몬스터 활성화
    { 
        Instantiate(prefabs[index]);
    }

}
