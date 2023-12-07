using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyManager : MonoBehaviour //Score&Money 상태 관리
{    
    [Header("Data")]
    [SerializeField] public GameData gameData; //Money와 HighScore은 DataManager에서 가져오기

    public static int Score; //현재 스코어 선언
    TextMeshProUGUI textComponent;
    
    //싱글톤 적용
    public static CurrencyManager instance;
    
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
        textComponent = GetComponent<TextMeshProUGUI>(); //초기화
        Score = 0;
    }

    void Update(){
        textComponent.text = "Score : " + Score.ToString();
    }

    private void MonsterRule(){ //몬스터 처치에 따른 점수부여

    }
    private void MoneyRule(){ //재화 규칙

    }

}
