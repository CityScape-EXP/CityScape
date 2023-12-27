using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class ScoreManager : MonoBehaviour //Score&Money 상태 관리
{    
    [Header("Data")]
    GameData gameData; //Money와 HighScore은 DataManager에서 가져오기???????????????????????????????

    public static int Score { get; set; } //현재 스코어 선언
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
            gameData.stageHighScore[(int)DataManager.NowStage] = Score;
            DataManager.instance.SaveGameData(gameData);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>(); //초기화
        Score = 0;
    }

    void Update(){
        textComponent.text = "Score : " + Score.ToString();
    }

}
