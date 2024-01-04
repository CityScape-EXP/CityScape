using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Reflection;

/*  DataManager.cs
 *  각종 json 파일을 관리하는 스크립트
 *  json파일이나 세이브 파일, 각종 게임이 꺼져도 유지되는 항목에 대해서는
 *  해당 스크립트에 작성한다
 */

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get { return GameManager.instance.dm; } set { GameManager.instance.dm = value; } }
    string _savePath = "";
    string savePath { get 
        { 
            if (instance._savePath == "") 
            {

#if UNITY_EDITOR
                instance._savePath = Application.dataPath;
#elif UNITY_ANDROID
                instance._savePath = Application.persistentDataPath;
#endif
            }
            return instance._savePath; } }
    public static Define.Stages NowStage { get; set; }

    GameData mainGameData = new GameData();

    public void Init()
    {
        if(instance._savePath == "")
        {
            Debug.Log("3");

            instance.mainGameData.money = PlayerPrefs.GetInt("Money", 0); //Money 초기화
            instance.mainGameData.isStageOpen[(int)Define.Stages.Stage1] = PlayerPrefs.GetInt($"IsStageOpen{Define.Stages.Stage1}", 1) != 0 ? true : false;
            instance.mainGameData.isStageOpen[(int)Define.Stages.Stage2] = PlayerPrefs.GetInt($"IsStageOpen{Define.Stages.Stage2}", 0) != 0 ? true : false;
            instance.mainGameData.isStageOpen[(int)Define.Stages.Stage3] = PlayerPrefs.GetInt($"IsStageOpen{Define.Stages.Stage3}", 0) != 0 ? true : false;

            instance.mainGameData.stageHighScore[(int)Define.Stages.Stage1] = PlayerPrefs.GetInt($"StageHighScore{Define.Stages.Stage1}", 0);
            instance.mainGameData.stageHighScore[(int)Define.Stages.Stage2] = PlayerPrefs.GetInt($"StageHighScore{Define.Stages.Stage2}", 0);
            instance.mainGameData.stageHighScore[(int)Define.Stages.Stage3] = PlayerPrefs.GetInt($"StageHighScore{Define.Stages.Stage3}", 0);

            instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.AttackSpeed] = PlayerPrefs.GetInt("AttackSpeed", 1);
            instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.Health] = PlayerPrefs.GetInt("Health", 1);
            instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.Power] = PlayerPrefs.GetInt("Power", 1);

        }
    }



    public static GameData MainGameData { 
        get { return instance.mainGameData; }
        set
        {
            instance.mainGameData.money = value.money;
            PlayerPrefs.SetInt("Money", instance.mainGameData.money);

            instance.mainGameData.isStageOpen[(int)Define.Stages.Stage1] = value.isStageOpen[(int)Define.Stages.Stage1];
            PlayerPrefs.SetInt($"IsStageOpen{Define.Stages.Stage1}", instance.mainGameData.isStageOpen[(int)Define.Stages.Stage1] ? 1 : 0);

            instance.mainGameData.isStageOpen[(int)Define.Stages.Stage2] = value.isStageOpen[(int)Define.Stages.Stage2];
            PlayerPrefs.SetInt($"IsStageOpen{Define.Stages.Stage2}", instance.mainGameData.isStageOpen[(int)Define.Stages.Stage2] ? 1 : 0);

            instance.mainGameData.isStageOpen[(int)Define.Stages.Stage3] = value.isStageOpen[(int)Define.Stages.Stage3];
            PlayerPrefs.SetInt($"IsStageOpen{Define.Stages.Stage2}", instance.mainGameData.isStageOpen[(int)Define.Stages.Stage3] ? 1 : 0);

            if (instance.mainGameData.stageHighScore[(int)Define.Stages.Stage1] < value.stageHighScore[(int)Define.Stages.Stage1])
            {
                instance.mainGameData.stageHighScore[(int)Define.Stages.Stage1] = value.stageHighScore[(int)Define.Stages.Stage1];
                PlayerPrefs.SetInt($"StageHighScore{Define.Stages.Stage1}", instance.mainGameData.stageHighScore[(int)Define.Stages.Stage1]);
            }

            if (instance.mainGameData.stageHighScore[(int)Define.Stages.Stage2] < value.stageHighScore[(int)Define.Stages.Stage2])
            {
                instance.mainGameData.stageHighScore[(int)Define.Stages.Stage2] = value.stageHighScore[(int)Define.Stages.Stage2];
                PlayerPrefs.SetInt($"StageHighScore{Define.Stages.Stage2}", instance.mainGameData.stageHighScore[(int)Define.Stages.Stage2]);
            }
            if (instance.mainGameData.stageHighScore[(int)Define.Stages.Stage3] < value.stageHighScore[(int)Define.Stages.Stage3])
            {
                instance.mainGameData.stageHighScore[(int)Define.Stages.Stage3] = value.stageHighScore[(int)Define.Stages.Stage3];
                PlayerPrefs.SetInt($"StageHighScore{Define.Stages.Stage3}", instance.mainGameData.stageHighScore[(int)Define.Stages.Stage3]);
            }

            /*초기화 함수
             * 
             */
            PlayerPrefs.DeleteKey("AttackSpeed");
            PlayerPrefs.DeleteKey("Health");
            PlayerPrefs.DeleteKey("Power");

            instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.AttackSpeed] = value.reinforceLevel[(int)Define.Reinforcement.AttackSpeed];
            PlayerPrefs.SetInt("AttackSpeed", instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.AttackSpeed]);

            instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.Health] = value.reinforceLevel[(int)Define.Reinforcement.Health];
            PlayerPrefs.SetInt("Health", instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.Health]);
            
            instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.Power] = value.reinforceLevel[(int)Define.Reinforcement.Power];
            PlayerPrefs.SetInt("Power", instance.mainGameData.reinforceLevel[(int)Define.Reinforcement.Power]);
        }
    }


#if UNITY_ANDROID
    // 안드로이드 빌드 떄 사용
    // savePath = Application.persistantDataPath;
#endif

    PatternData PatternData = null;
    public void PatternDataClear() 
    {
        PatternData = null;
    }
    // Json 파일을 통해 PatternData를 가져오는 과정
    // Json 파일의 이름 규칙은 다음과 같다. (예) St0_Phase2_Pattern0
    public PatternData GetPatternData(int stage, int phase, int pattern)
    {

        TextAsset textAsset = Resources.Load<TextAsset>($"Patterns/St{stage}_Phase{phase}_Pattern{pattern}");
        if (textAsset == null)
        {
            Debug.Log("null__");
        }
        string jsonData = textAsset.text;

        PatternData = JsonUtility.FromJson<PatternData>(jsonData);

        return PatternData;
    }

    // GameData.json 파일을 GameData 클래스 정보로 변경하는 함수
   
    public GameData GetGameData() { return MainGameData; }


    /*
     *  저장 부분 : 변화가 일어날 때에는 무조건 저장을 한다
     *  -> 일어난 변화를 적용시켜줘야 한다
     */
    // GameData 클래스 정보를 받아 GameData.json에 저장하는 함수
    public void SaveGameData(GameData gdata) { 
        MainGameData = gdata;
        PatternDataClear();
    }
    public void SaveGameData()
    {
        ScoreManager.instance.GameOverDataSave();
        //PlayerPrefs.SetInt("Money", mainGameData.money);
    }

    void SaveGameDataUsePlayerPrefs(GameData gdata)
    {
        string savePath = Application.dataPath;
        string data = JsonUtility.ToJson(gdata);
        File.WriteAllText(savePath + "/Resources/Data/GameData.json", data);
        GameManager.instance.gameData = GetGameData();
    }

    public void SavePatternData(PatternData patternData, int stage, int phase, int pattern)
    {
         
        string data = JsonUtility.ToJson(patternData);
        Debug.Log(data);
        string filePath = savePath + $"/Resources/Patterns/St{stage}_Phase{phase}_Pattern{pattern}.json";
        if(!File.Exists(filePath))
        {
            File.Create(filePath);
            Debug.Log("파일 생성.. 위치 : " + savePath);
        }
        File.WriteAllText(filePath, data);
        Debug.Log("테스트 데이터 저장 완료");
    }

}


// GameData 클래스
[System.Serializable]
public class GameData
{   
    public List<bool> isStageOpen;
    public List<int> stageHighScore;
    public int money;

    public List<int> reinforceLevel;
    
    // 초기화를 위한 생성자
    public GameData()
    {
        reinforceLevel = new List<int>() { 1,1,1 };
        isStageOpen = new List<bool>() { true, false, false};
        stageHighScore = new List<int>() { 0, 0, 0 };
        money = 0;
    }


    /// <summary>
    /// GameData newGameData = new GameData();
    /// int num = newGameData[Define.Stages.Stage1] 는 이제 하이스코어가 출력됩니다.
    /// newGameData[Define.Stages.Stage1] = (int) _intnum 는 이제 하이스코어가 저장됩니다.(PlayerPrebs 기반)
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int this [Define.Stages index]
    {
        get
        {
            return stageHighScore[(int)index];
        }

        set
        {
            if(stageHighScore[(int)index] < value)
            {
                stageHighScore[(int)index] = value;
                PlayerPrefs.SetInt($"StageHighScore{index}", stageHighScore[(int)index]);
            }
        }
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

