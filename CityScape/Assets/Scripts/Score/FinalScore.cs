using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScore : MonoBehaviour //결과UI에 최종 스코어를 띄우기 위한 스크립트
{
    public static int finalScore; //스테이지진행도규칙을 추가한 최종스코어 변수 선언
    TextMeshProUGUI textComponent;

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
        textComponent = GetComponent<TextMeshProUGUI>(); 
        finalScore = 0; //초기화
        StageAdvRule();
        textComponent.text = finalScore.ToString();
    }

  
    public void StageAdvRule(){ //게임 중 계속 바뀌므로 통폐합 이제 안씀
        ScoreManager.instance.TimeScore();
    }
}
