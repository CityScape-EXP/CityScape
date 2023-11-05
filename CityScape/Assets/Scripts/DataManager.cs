using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    static string savePath = Application.dataPath;
#if UNITY_ANDROID
    // 안드로이드 빌드 떄 사용
    // savePath = Application.persistantDataPath;
#endif
    // Json 파일을 통해 PatternData를 가져오는 과정
    // Json 파일의 이름 규칙은 다음과 같다. (예) St0_Phase2_Pattern0
    public static PatternData GetPatternData(int stage, int phase, int pattern)
    {
        PatternData data = new PatternData();
        string path = savePath + $"/Resources/St{stage}_Phase{phase}_Pattern{pattern}.json" ;
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<PatternData>(jsonData);
        return data;
    }

    public static PlayerData GetPlayerData()
    {
        PlayerData data = new PlayerData();
        string path = savePath + $"/Resources/PlayerData.json";
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<PlayerData>(jsonData);
        return data;
    }

    public static GameData GetGameData()
    {
        GameData data = new GameData();
        string path = savePath + $"/Resources/GameData.json";
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<GameData>(jsonData);
        return data;
    }

    public static void SaveGameData(ref GameData gdata)
    {
        string data = JsonUtility.ToJson(gdata);
        File.WriteAllText(data, savePath + "/Resources/GameData.json");
    }

    public static void SavePlayerData(ref PlayerData pdata)
    {
        string data = JsonUtility.ToJson(pdata);
        File.WriteAllText(data, savePath + "/Resources/PlayerData.json");
    }
}

[System.Serializable]
public class PlayerData
{
    public int playerMaxHp;
    public float playerOffence;
    public float playerAttackSpeed;
}

[System.Serializable]
public class GameData
{
    public List<bool> isStageOpen = new List<bool>();
    public List<int> stageHighScore = new List<int>();
    public int money;
    public int systemSetting_Audio0;
    public int systemSetting_Audio1;
    public int systemSetting_Audio2;
}

[System.Serializable]
public class PatternData
{
    public List<PlatformData> p_Data = new List<PlatformData>();
    public List<EnemyData> e_Data = new List<EnemyData>();
    public int patternTime;
    
    [System.Serializable]
    public class PlatformData
    {
        public int floor;
        public float pos;
        public int width;
    }

    [System.Serializable]
    public class EnemyData
    {
        public int type;
        public float x_pos; 
        public float y_pos;
    }
}

