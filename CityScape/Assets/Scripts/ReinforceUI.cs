using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ReinforceUI : MonoBehaviour
{
    // 변경한다면 Reinforcement의 upgradeMoneyData도 같이 변경할것
    int[,] upgradeMoneyData = new int[3, 4]
        { {10, 20, 30, 50 },
          {10, 20, 30, 50 },
          {10, 20, 30, 50 } };
    public enum InfoType { Level, UpInfo, Money, UpCost1,UpCost2 }
    public int field;
    public InfoType type;
    TMP_Text myText;
    
    private TextMeshProUGUI textComponent;

    void Start(){
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    void Update(){
        textComponent.text = "◎ :  " + DataManager.MainGameData.money.ToString();
    }

    void Awake()
    {
        myText = GetComponent<TMP_Text>();
    }

    // Save는 필요 없고, Load만을 사용하면 된다 : 정보 표출이기 때문
    void LateUpdate()
    {
        int level = 0;
        switch (field)
        {
            case 0: level = DataManager.MainGameData.reinforceLevel[(int)Define.Reinforcement.Health]; break;
            case 1: level = DataManager.MainGameData.reinforceLevel[(int)Define.Reinforcement.Power]; break;
            case 2: level = DataManager.MainGameData.reinforceLevel[(int)Define.Reinforcement.AttackSpeed]; break;
        }
        switch (type)
        {
            // 레벨 정보 텍스트 (패널별 중앙의 왼쪽에 위치) ex) Lv.1, MAX ..
            case InfoType.Level:
                if (level == 5) myText.text = "MAX";
                else myText.text = $"Lv.{level}";
                break;

            // 업그레이드 정보 텍스트 (패널별 중앙에 위치) ex) 105% > 110% ..
            case InfoType.UpInfo:
                switch(field)
                {
                    case 0:
                        if (level == 5)
                            myText.text = "7";
                        else
                            myText.text = $"{3 + level - 1} > <color=#336699>{3 + level}</color>";
                        break;
                    case 1:
                        if (level == 5)
                            myText.text = $"{100 + (level - 1) * 5}";
                        else
                            myText.text = $"{100 + (level - 1) * 5}% > <color=#336699>{100 + level * 5}%</color>";
                        break;
                    case 2:
                        if (level == 5)
                            myText.text = $"{100 + (level - 1) * 25}";
                        else
                            myText.text = $"{100 + (level - 1) * 25}% > <color=#336699>{100 + level * 25}%</color>";
                        break;
                }
                break;

            // 현재 소유 재화 정보 텍스트 (우측 상단에 위치) ex) ◎ : 999 ..
            case InfoType.Money:
                myText.text = $"{PlayerPrefs.GetInt("Money")}";
                break;

            // 업그레이드 필요 재화 정보 텍스트 (패널별 우측에 위치) ex) Lv.3 50필요
            case InfoType.UpCost1:
                string lv;
                if (level == 5) // 만렙일경우 -> 소모 재화량 표시 X
                {
                    lv = "MAX";
                    myText.text = lv;
                    break;
                }
                else
                {
                    lv = $"Lv.{level}";
                    myText.text = $"{lv}";
                }
                break;
            case InfoType.UpCost2:
                if(level == 5)
                {
                    myText.text = "";
                    break;
                }
                int cost = upgradeMoneyData[field, level - 1];
                if (PlayerPrefs.GetInt("Money") < cost) // 돈이 부족할경우 빨간색으로
                    myText.text = $"<color=#cc0000>{cost}</color>";
                else myText.text = $"<color=#336699>{cost}</color>";
                break;
        }
    }
}
