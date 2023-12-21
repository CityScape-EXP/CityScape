using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScore : MonoBehaviour //결과UI에 최종 스코어를 띄우기 위한 스크립트
{
    public static int finalScore; //스테이지진행도규칙을 추가한 최종스코어 변수 선언

    //싱글톤 적용
    public static FinalScore instance;
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        finalScore = 0; //초기화
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        StageAdvRule();
        textComponent.text = "Final Score : " + finalScore.ToString();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageAdvRule(){ //stage진행도에 따른 점수 추가부여
        double timeP = (GameManager.instance.stageTime / GRManager.instance.gameTime) * 100; // 게임시간 테스트용 변동 가능 => /0.12
        float timeR = (float)Math.Truncate(timeP*10)/10; //소수점 첫째자리 이하 버림
        
        if(timeP >= 100){ //100%면 10000점 추가
            ScoreManager.Score += 10000;
        }
        else{
            ScoreManager.Score += Mathf.RoundToInt(timeR * 100);
        }
        finalScore = ScoreManager.Score;
    }
}
