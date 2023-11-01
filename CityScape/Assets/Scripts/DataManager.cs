using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    // Json 파일을 통해 PatternData를 가져오는 과정
    // Json 파일의 이름 규칙은 다음과 같다. (예) St0_Phase2_Pattern0
    public static PatternData GetPatternData(int stage, int phase, int pattern)
    {
        PatternData data = new PatternData();
        string path = Application.dataPath + $"/Resources/St{stage}_Phase{phase}_Pattern{pattern}.json" ;
        string jsonData = File.ReadAllText(path);
        data = JsonUtility.FromJson<PatternData>(jsonData);
        return data;
    }
}


[System.Serializable]
public class PatternData
{
    public List<PlatformData> p_Data = new List<PlatformData>();
    public List<EnemyData> e_Data = new List<EnemyData>();
    
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

