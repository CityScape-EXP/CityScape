using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*  
 *  Reinforcement.cs
 *  ���� ��ȭ�� ���õ� �Լ��� �ۼ��Ѵ�
 *  ��ȭ�� ������ ������ ���� ���� �ݵ�� Save�� ����Ǿ�� ��
 */
public class Reinforcement : MonoBehaviour
{
    // �����Ѵٸ� HUI�� upgradeMoneyData�� ���� �����Ұ�
    // ��ȭ�� �ʿ��� ��ȭ ����Ÿ
    int[,] upgradeMoneyData = new int[3, 4]
        { {10, 20, 30, 50 }, 
          {10, 20, 30, 50 }, 
          {10, 20, 30, 50 } };
    Button thisButton;
    int nowLevel;
    public int field;  // ��ȭ�ϴ� �׸� ���� ���� => 0 = Hp, 1 = Atk, 2 = AS
    GameManager gameManager;    // ���� ��ȭ ������ �����ϱ� ���� gameManager

    void Start()
    {
        thisButton = gameObject.GetComponent<Button>();
        gameManager = GameManager.instance;
        // ���� ��������
        switch (field)
        {
            case 0: nowLevel = gameManager.upgradeData.hpLevel; break;
            case 1: nowLevel = gameManager.upgradeData.offenceLevel; break;
            case 2: nowLevel = gameManager.upgradeData.asLevel; break;
        }
        if (nowLevel == 5)
            thisButton.interactable = false;
        Debug.Log($"�ʵ� : {field}, ���� : {nowLevel}");
    }

    // �׽�Ʈ�� �Լ� : �� 100 ����
    public void Cheat()
    {
        gameManager.gameData.money += 100;
        gameManager.dm.SaveGameData(gameManager.gameData);
    }

    // Upgrade �Լ�
    public void GetUpgrade()
    {
        int nowMoney = gameManager.gameData.money;
        // ���� �����ϸ� Error ��� �� ����
        if(nowMoney < upgradeMoneyData[field, nowLevel-1]) 
        {
            Debug.Log("���� �����մϴ�");
            return;
        }

        // ��ȭ & json�� ���� Save
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
        Debug.Log($"�ʵ� {field}�� ��ȭ�� �����ϼ̽��ϴ�! Lv.{nowLevel} -> Lv.{nowLevel + 1}");
        nowLevel++;

        // ���� �޼�
        if (nowLevel == 5)
            thisButton.interactable = false;
    }
}
