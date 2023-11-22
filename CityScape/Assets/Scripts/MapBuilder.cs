using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PrefebElement
{
    public string m_Name;
    public GameObject m_Prefeb;
}

public class MapBuilder : MonoBehaviour
{
    // ���� ������ (��, ��, �Ĺ� ������ �б���)
    public static MapBuilder instance = null;
    public int acmPattern;
    private int nowPhase;
    private float patternStartTime;
    private float nowPatternTime;
    public void Init_var()
    {
        acmPattern = 0;
        nowPhase = 0;
        patternStartTime = 0f;
        nowPatternTime = 0f;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        Init_var();
    }

    private void Update()
    {
        // TestScene(�ΰ���)�� ��쿡�� MapBuilder �����Ѵ�
        if (!(SceneManager.GetActiveScene().name == "TestScene"))
            return;

        // ���Ͽ� �Ҵ�� �ð��� �����ٸ�
        if(Time.time - patternStartTime > nowPatternTime)
        {
            //�ʹ�, �߹�, �Ĺ� �б� ����
            // 5��, 8��, 6���� ����
            if (acmPattern < 5) nowPhase = 0;
            else if (acmPattern < 13) nowPhase = 1;
            else if (acmPattern < 19) nowPhase = 2;
            else Debug.Log("�������� Ŭ����");

            int patternNum = Random.Range(0, 4);
            Debug.Log($"패턴 {patternNum}번 생성!");
            PatternData nowPattern = GameManager.instance.dm.GetPatternData(0, nowPhase, patternNum);
            DrawPattern(nowPattern);

            nowPatternTime = nowPattern.patternTime;
            patternStartTime = Time.time;
            acmPattern++;
        }
    }

    private void DrawPattern(PatternData pd)
    {
        foreach (var platform in pd.p_Data)
        {
            GameObject platformObject = PoolManager.GetObject(4);
            platformObject.transform.position = new Vector3(platform.pos + 20, platform.floor * 1.5f, 0);
        }
        foreach (var enemy in pd.e_Data)
        {
            GameObject enemyObject = PoolManager.GetObject(2 + enemy.type);
            enemyObject.transform.position = new Vector3(enemy.x_pos + 20, enemy.y_pos, 0);
        }
    }

    
}
