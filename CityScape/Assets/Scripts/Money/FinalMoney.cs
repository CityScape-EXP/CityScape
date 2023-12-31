using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalMoney : MonoBehaviour //초기화 방지를 위한 새로운 코인관련 스크립트
{
    public GameData gameData;
    public static int finalMoney { get; set; } = 0; //얻은 돈(누적x)

    //싱글톤 적용
    public static FinalMoney instance;
    
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
        finalMoney = GetMoney.getMoney;
    }

}
