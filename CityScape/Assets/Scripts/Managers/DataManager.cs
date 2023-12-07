using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

/*  DataManager.cs
 *  각종 json 파일을 관리하는 스크립트
 *  json파일이나 세이브 파일, 각종 게임이 꺼져도 유지되는 항목에 대해서는
 *  해당 스크립트에 작성한다
 */

public class DataManager : MonoBehaviour
{
    //싱글톤
    public static DataManager instance;
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
        }
    }

#if UNITY_ANDROID
    // 안드로이드 빌드 떄 사용
    // savePath = Application.persistantDataPath;
#endif
    // Json 파일을 통해 PatternData를 가져오는 과정
    // Json 파일의 이름 규칙은 다음과 같다. (예) St0_Phase2_Pattern0
    public PatternData GetPatternData(int stage, int phase, int pattern)
    {
        string savePath = Application.dataPath;
        PatternData data = new PatternData();
        string path = savePath + $"/Resources/Patterns/St{stage}_Phase{phase}_Pattern{pattern}.json" ;
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<PatternData>(jsonData);
        return data;
    }

    // UpgradeData.json 파일을 UpgradeData 클래스 정보로 변경하는 함수
    public UpgradeData GetUpgradeData()
    {
        string savePath = Application.dataPath;
        UpgradeData data = new UpgradeData();
        string path = savePath + $"/Resources/Data/UpgradeData.json";
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<UpgradeData>(jsonData);
        return data;
    }

    // GameData.json 파일을 GameData 클래스 정보로 변경하는 함수
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
     *  저장 부분 : 변화가 일어날 때에는 무조건 저장을 한다
     *  -> 일어난 변화를 적용시켜줘야 한다
     */
    // GameData 클래스 정보를 받아 GameData.json에 저장하는 함수
    public void SaveGameData(GameData gdata)
    {
        string savePath = Application.dataPath;
        string data = JsonUtility.ToJson(gdata);
        Debug.Log("저장 데이터 : " + data);
        File.WriteAllText(savePath + "/Resources/Data/GameData.json", data);
        GameManager.instance.gameData = GetGameData();
    }

    // UpgradeData 클래스 정보를 받아 UpgradeData.json에 저장하는 함수
    public void SaveUpgradeData(UpgradeData udata)
    {
        string savePath = Application.dataPath;
        string data = JsonUtility.ToJson(udata);
        Debug.Log("저장 데이터 : " + data);
        File.WriteAllText(savePath + "/Resources/Data/UpgradeData.json", data);
        GameManager.instance.upgradeData = GetUpgradeData();
    }

    public void SavePatternData(PatternData patternData)
    {
        string savePath = Application.dataPath;
        string data = JsonUtility.ToJson(patternData);
        Debug.Log(data);
        string filePath = savePath + "/Resources/Patterns/TestPattern.json";
        if(!File.Exists(filePath))
        {
            File.Create(filePath);
            Debug.Log("파일 생성.. 위치 : " + savePath);
        }
        File.WriteAllText(filePath, data);
        Debug.Log("테스트 데이터 저장 완료");
    }

    // 게임 첫 시작시 UpgradeData를 초기화하는 함수
    public void InitUpgradeData()
    {
        Debug.Log("강화 수치 초기화");
        UpgradeData p_temp = new UpgradeData();
        SaveUpgradeData(p_temp);
    }

    // 게임 첫 시작시 GameData를 초기화하는 함수
    public void InitGameData()
    {
        Debug.Log("게임 데이터 초기화");
        GameData g_temp = new GameData();
        SaveGameData(g_temp);
    }
}

// UpgradeData 클래스
[System.Serializable]
public class UpgradeData
{
    public int hpLevel;
    public int offenceLevel;
    public int asLevel;
    
    // 초기화를 위한 생성자
    public UpgradeData(int hp = 1, int atk = 1, int atks = 1)
    {
        hpLevel = hp; offenceLevel = atk; asLevel = atks;
    }
}

// GameData 클래스
[System.Serializable]
public class GameData
{   
    public List<bool> isStageOpen;
    public List<int> stageHighScore;
    public int money;
    
    // 초기화를 위한 생성자
    public GameData()
    {
        isStageOpen = new List<bool>() { true, false, false};
        stageHighScore = new List<int>() { 0, 0, 0 };
        money = 0;
    }
}


// 게임 패턴을 저장하는 PatternData 클래스
[System.Serializable]
public class PatternData
{
    public List<PlatformData> p_Data = new List<PlatformData>();
    public List<EnemyData> e_Data = new List<EnemyData>();
    public float patternTime;
    
    // 플랫폼 정보를 저장하는 PlatformData 클래스
    [System.Serializable]
    public class PlatformData
    {
        public int type;
        public int floor;
        public float pos;
        public int width;
    }

    // 몬스터 정보를 저장하는 EnemyData 클래스
    [System.Serializable]
    public class EnemyData
    {
        public int type;
        public float x_pos; 
        public int floor;
    }
}

