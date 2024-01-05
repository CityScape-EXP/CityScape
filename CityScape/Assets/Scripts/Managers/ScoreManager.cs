using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour //Score&Money 상태 관리
{    
    [Header("Data")]
    GameData gameData; //Money와 HighScore은 DataManager에서 가져오기???????????????????????????????

    public int decreasedScore;

    private int _score;
    public int Score { 
        get { return ObjectScore + _timeScore - decreasedScore; } 
        set {
            _score = value;
        }
    }

    public static int ObjectScore { get; set; } = 0; //오브젝트 스코어 선언, 아이템 등 오브젝트를 통해 얻은 점수
    int _timeScore = 0; 
    public int timeScore 
    {
        get { return _timeScore; }
        set { _timeScore = value; }
    }
    TextMeshProUGUI textComponent;
    
    //싱글톤 적용
    public static ScoreManager instance;
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameOverDataSave()
    {
        gameData = DataManager.MainGameData;
        if(Score > gameData.stageHighScore[(int)DataManager.NowStage])
        {
            gameData[DataManager.NowStage] = Score;
            
        }
        DataManager.instance.SaveGameData(gameData);
    }


    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>(); //초기화
        ObjectScore = 0;
        _timeScore = 0;
    }

    void Update(){
        if (Score < 0){
            ObjectScore = 0;
            _timeScore = 0;
            Debug.Log("스코어 0 이하로 떨어짐:" + Score);
        }
        //Debug.Log("스코어 현재상황:" + Score + " @"+ _timeScore+"*"+ObjectScore);
        
        TimeScore();
        textComponent.text = $"Score : {Score}" ;
    }

    public void TimeScore()
    { //스테이지 진행도에 따라 점수 추가부여
        double timeP = (GameManager.instance.stageTime / GRManager.instance.gameTime) * 100; // 게임시간 테스트용 변동 가능 => /0.12
        float timeR = (float)Math.Truncate(timeP * 10) / 10; //소수점 첫째자리 이하 버림
        _timeScore = Mathf.RoundToInt(timeR * 100); //시간별 점수 추가
    }

}
