using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*  
 *  Reinforcement.cs
 *  각종 강화와 관련된 함수를 작성한다
 *  재화나 정보의 변경이 있을 때는 반드시 Save도 병행되어야 함
 */
public class Reinforcement : MonoBehaviour
{
    // 변경한다면 HUI의 upgradeMoneyData도 같이 변경할것
    // 강화에 필요한 재화 데이타
    int[,] upgradeMoneyData = new int[3, 4]
        { {10, 20, 30, 50 }, 
          {10, 20, 30, 50 }, 
          {10, 20, 30, 50 } };
    Button thisButton;
    int nowLevel;
    public int field;  // 강화하는 항목에 대한 정보 => 0 = Hp, 1 = Atk, 2 = AS
    GameManager gameManager;    // 현재 강화 정보에 접근하기 위한 gameManager

    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        gameManager = GameManager.instance;
        // 레벨 가져오기
        switch (field)
        {
            case 0: nowLevel = gameManager.upgradeData.hpLevel; break;
            case 1: nowLevel = gameManager.upgradeData.offenceLevel; break;
            case 2: nowLevel = gameManager.upgradeData.asLevel; break;
        }
        if (nowLevel == 5)
            thisButton.interactable = false;
        Debug.Log($"필드 : {field}, 레벨 : {nowLevel}");
    }

    // 테스트용 함수 : 돈 100 증가
    public void Cheat()
    {
        gameManager.gameData.money += 100;
        gameManager.dm.SaveGameData(gameManager.gameData);
    }

    // Upgrade 함수
    public void GetUpgrade()
    {
        int nowMoney = gameManager.gameData.money;
        // 돈이 부족하면 Error 출력 후 종료
        if(nowMoney < upgradeMoneyData[field, nowLevel-1]) 
        {
            Debug.Log("돈이 부족합니다");
            GameManager.Sound.Play(Define.SFX.UI_upgrade_fail_1128);
            return;
        }
        GameManager.Sound.Play(Define.SFX.UI_upgrade_1128);
        // 강화 & json에 정보 Save
        nowMoney -= upgradeMoneyData[field, nowLevel-1];
        gameManager.gameData.money = nowMoney;
        switch(field)
        {
            case 0: gameManager.upgradeData.hpLevel = nowLevel + 1; break;
            case 1: gameManager.upgradeData.offenceLevel = nowLevel + 1; break;
            case 2: gameManager.upgradeData.asLevel = nowLevel + 1; break;
        }
        gameManager.dm.SaveUpgradeData(gameManager.upgradeData);
        gameManager.dm.SaveGameData(gameManager.gameData);
        Debug.Log($"필드 {field}번 강화에 성공하셨습니다! Lv.{nowLevel} -> Lv.{nowLevel + 1}");
        nowLevel++;

        // 만렙 달성
        if (nowLevel == 5)
            thisButton.interactable = false;
    }
}
