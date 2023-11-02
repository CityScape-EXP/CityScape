using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DataManager : MonoBehaviour
{
    public List<PatternData> early_Patterns;
    public List<PatternData> mid_Patterns;
    public List<PatternData> late_Patterns;
    public PatternData patternA;

    private void Start()
    {
        string test = JsonUtility.ToJson(patternA);
        // Debug.Log(test); 烙矫 林籍 贸府
        PatternData map2 = JsonUtility.FromJson<PatternData>(test);
        // Debug.Log(map2.p_Data[1].width); 烙矫 林籍 贸府
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