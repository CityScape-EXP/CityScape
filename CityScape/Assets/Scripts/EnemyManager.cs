using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] prefabs;
    float time;

    // private float gameTime; ���� �ڵ�� �ּ� ó��

    void Start()
    {
        //  if (time > 3f) // 3�ʿ� ���� �����ϴ� �ڵ� ���׳��� �ּ�ó��
        //  {
        onStage(0); // ������ 0��° ���� ����
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

    public void onStage(int index) // ���� Ȱ��ȭ
    { 
        Instantiate(prefabs[index]);
    }

}
