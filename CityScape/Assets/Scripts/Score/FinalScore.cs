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
        Debug.Log(finalScore);
        textComponent.text = "Final Score : " + finalScore.ToString();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StageAdvRule(){ //stage진행도에 따른 점수추가부여
        float timePercent = (GameManager.instance.stageTime / GRManager.instance.gameTime) * 100;
        int roundedNumber = Mathf.FloorToInt(timePercent);

        if(timePercent == 100){ //100%면 10000점 추가
            CurrencyManager.Score += 10000;
        }
        else{
            CurrencyManager.Score += roundedNumber*100; //1%당 100점씩 추가
            CurrencyManager.Score += (int)((timePercent - roundedNumber) * 100); //0.1%당 10점씩 추가
        }
        finalScore = CurrencyManager.Score;
    }
}
