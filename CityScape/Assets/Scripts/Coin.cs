using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Collider2D coll;
    float moveSpeed = 5f;   // 배경 이동속도와 같다
    public int moneyAmount; // 코인의 돈 (5 or 10)
    
    private void Awake()
    {
        coll = GetComponent<Collider2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        // 동전도 이동 속도인 5f 속도에 따라 Translate 한다
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    
    /*//동전이 캐릭터와 충돌하면 점수 오르고 돈 오른다 => player 스크립트에 구현
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            GameManager.instance.gameData.money += 5;
            GameManager.instance.dm.SaveGameData(GameManager.instance.gameData);
        }
    }*/
}
