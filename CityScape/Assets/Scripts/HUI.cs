using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUI : MonoBehaviour
{
    // �����Ѵٸ� Reinforcement�� upgradeMoneyData�� ���� �����Ұ�
    int[,] upgradeMoneyData = new int[3, 4]
        { {10, 20, 30, 50 },
          {10, 20, 30, 50 },
          {10, 20, 30, 50 } };
    public enum InfoType { Level, UpInfo, Money, UpCost }
    public int field;
    public InfoType type;

    TMP_Text myText;

    void Awake()
    {
        myText = GetComponent<TMP_Text>();
    }

    // Save�� �ʿ� ����, Load���� ����ϸ� �ȴ� : ���� ǥ���̱� ����
    void LateUpdate()
    {
        int level = 0;
        switch (field)
        {
            case 0: level = GameManager.instance.upgradeData.hpLevel; break;
            case 1: level = GameManager.instance.upgradeData.offenceLevel; break;
            case 2: level = GameManager.instance.upgradeData.asLevel; break;
        }
        switch (type)
        {
            // ���� ���� �ؽ�Ʈ (�гκ� �߾��� ���ʿ� ��ġ) ex) Lv.1, MAX ..
            case InfoType.Level:
                if (level == 5) myText.text = "MAX";
                else myText.text = $"Lv.{level}";
                break;

            // ���׷��̵� ���� �ؽ�Ʈ (�гκ� �߾ӿ� ��ġ) ex) 105% > 110% ..
            case InfoType.UpInfo:
                switch(field)
                {
                    case 0:
                        if (level == 5)
                            myText.text = "7";
                        else
                            myText.text = $"{3 + level - 1} > <color=#00ff00>{3 + level}</color>";
                        break;
                    case 1:
                        if (level == 5)
                            myText.text = $"{100 + (level - 1) * 5}";
                        else
                            myText.text = $"{100 + (level - 1) * 5}% > <color=#00ff00>{100 + level * 5}%</color>";
                        break;
                    case 2:
                        if (level == 5)
                            myText.text = $"{100 + (level - 1) * 25}";
                        else
                            myText.text = $"{100 + (level - 1) * 25}% > <color=#00ff00>{100 + level * 25}%</color>";
                        break;
                }
                break;

            // ���� ���� ��ȭ ���� �ؽ�Ʈ (���� ��ܿ� ��ġ) ex) �� : 999 ..
            case InfoType.Money:
                myText.text = $"�� : {GameManager.instance.gameData.money}";
                break;

            // ���׷��̵� �ʿ� ��ȭ ���� �ؽ�Ʈ (�гκ� ������ ��ġ) ex) Lv.3 50�ʿ�
            case InfoType.UpCost:
                string lv;
                if (level == 5) // �����ϰ�� -> �Ҹ� ��ȭ�� ǥ�� X
                {
                    lv = "MAX";
                    myText.text = lv;
                    break;
                }
                lv = $"Lv.{level}";
                int cost = upgradeMoneyData[field, level - 1];
                if (GameManager.instance.gameData.money < cost) // ���� �����Ұ�� ����������
                    myText.text = $"{lv}\n<color=#ff0000>{cost}�ʿ�</color>";
                else myText.text = $"{lv}\n{cost}�ʿ�";
                break;
        }
    }
}
