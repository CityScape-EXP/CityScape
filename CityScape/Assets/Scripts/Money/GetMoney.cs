using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetMoney : MonoBehaviour
{
    //public GameData gameData;
    public static int getMoney { get; set; } = 0; //얻은 돈(누적x)

    private TextMeshProUGUI textComponent;

    //싱글톤 적용
    public static GetMoney instance;
    
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
        Reset();
        //getMoney += DataManager.MainGameData.money;
        textComponent = GetComponent<TextMeshProUGUI>();
        //DataManager.MainGameData.money += getMoney; //코인 누적
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = getMoney.ToString();
    }

    void Reset(){
        getMoney = 0; //초기화
        Debug.Log(GetMoney.getMoney+"음"+getMoney+"음"+DataManager.MainGameData.money); //왜 초기화가 안되지
    }

    public static int GetMoneyValue 
    { 
        get { return getMoney; } 
        set 
        { 
            getMoney = value; 
            DataManager.MainGameData.money += getMoney; // DataManager에 업뎃
            DataManager.instance.SaveGameData(DataManager.MainGameData); // 저장
        } 
    }
}
