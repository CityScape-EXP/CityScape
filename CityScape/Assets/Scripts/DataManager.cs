using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

/*  DataManager.cs
 *  ���� json ������ �����ϴ� ��ũ��Ʈ
 *  json�����̳� ���̺� ����, ���� ������ ������ �����Ǵ� �׸� ���ؼ���
 *  �ش� ��ũ��Ʈ�� �ۼ��Ѵ�
 */

public class DataManager : MonoBehaviour
{
    
#if UNITY_ANDROID
    // �ȵ���̵� ���� �� ���
    // savePath = Application.persistantDataPath;
#endif
    // Json ������ ���� PatternData�� �������� ����
    // Json ������ �̸� ��Ģ�� ������ ����. (��) St0_Phase2_Pattern0
    public PatternData GetPatternData(int stage, int phase, int pattern)
    {
        string savePath = Application.dataPath;
        PatternData data = new PatternData();
        string path = savePath + $"/Resources/Patterns/St{stage}_Phase{phase}_Pattern{pattern}.json" ;
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<PatternData>(jsonData);
        return data;
    }

    // UpgradeData.json ������ UpgradeData Ŭ���� ������ �����ϴ� �Լ�
    public UpgradeData GetUpgradeData()
    {
        string savePath = Application.dataPath;
        UpgradeData data = new UpgradeData();
        string path = savePath + $"/Resources/Data/UpgradeData.json";
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<UpgradeData>(jsonData);
        return data;
    }

    // GameData.json ������ GameData Ŭ���� ������ �����ϴ� �Լ�
    public GameData GetGameData()
    {
        string savePath = Application.dataPath;
        GameData data = new GameData();
        string path = savePath + $"/Resources/Data/GameData.json";
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<GameData>(jsonData);
        return data;
    }


    /*
     *  ���� �κ� : ��ȭ�� �Ͼ ������ ������ ������ �Ѵ�
     *  -> �Ͼ ��ȭ�� ���������� �Ѵ�
     */
    // GameData Ŭ���� ������ �޾� GameData.json�� �����ϴ� �Լ�
    public void SaveGameData(GameData gdata)
    {
        string savePath = Application.dataPath;
        string data = JsonUtility.ToJson(gdata);
        Debug.Log("���� ������ : " + data);
        File.WriteAllText(savePath + "/Resources/Data/GameData.json", data);
        GameManager.instance.gameData = GetGameData();
    }

    // UpgradeData Ŭ���� ������ �޾� UpgradeData.json�� �����ϴ� �Լ�
    public void SaveUpgradeData(UpgradeData udata)
    {
        string savePath = Application.dataPath;
        string data = JsonUtility.ToJson(udata);
        Debug.Log("���� ������ : " + data);
        File.WriteAllText(savePath + "/Resources/Data/UpgradeData.json", data);
        GameManager.instance.upgradeData = GetUpgradeData();
    }

    // ���� ù ���۽� UpgradeData�� �ʱ�ȭ�ϴ� �Լ�
    public void InitUpgradeData()
    {
        Debug.Log("��ȭ ��ġ �ʱ�ȭ");
        UpgradeData p_temp = new UpgradeData();
        SaveUpgradeData(p_temp);
    }

    // ���� ù ���۽� GameData�� �ʱ�ȭ�ϴ� �Լ�
    public void InitGameData()
    {
        Debug.Log("���� ������ �ʱ�ȭ");
        GameData g_temp = new GameData();
        SaveGameData(g_temp);
    }
}

// UpgradeData Ŭ����
[System.Serializable]
public class UpgradeData
{
    public int hpLevel;
    public int offenceLevel;
    public int asLevel;
    
    // �ʱ�ȭ�� ���� ������
    public UpgradeData(int hp = 1, int atk = 1, int atks = 1)
    {
        hpLevel = hp; offenceLevel = atk; asLevel = atks;
    }
}

// GameData Ŭ����
[System.Serializable]
public class GameData
{
    public List<bool> isStageOpen;
    public List<int> stageHighScore;
    public int money;
    
    // �ʱ�ȭ�� ���� ������
    public GameData()
    {
        isStageOpen = new List<bool>() { false, false, false};
        stageHighScore = new List<int>() { 0, 0, 0 };
        money = 0;
    }
}


// ���� ������ �����ϴ� PatternData Ŭ����
[System.Serializable]
public class PatternData
{
    public List<PlatformData> p_Data = new List<PlatformData>();
    public List<EnemyData> e_Data = new List<EnemyData>();
    public int patternTime;
    
    // �÷��� ������ �����ϴ� PlatformData Ŭ����
    [System.Serializable]
    public class PlatformData
    {
        public int floor;
        public float pos;
        public int width;
    }

    // ���� ������ �����ϴ� EnemyData Ŭ����
    [System.Serializable]
    public class EnemyData
    {
        public int type;
        public float x_pos; 
        public float y_pos;
    }
}

