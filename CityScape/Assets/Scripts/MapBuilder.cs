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
        Debug.Log("init_var");
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
        Debug.Log("DrawPattern 실행");
        foreach (var platform in pd.p_Data)
        {
            GameObject platformObject = PoolManager.GetObject(5);
            Vector3 platformPos;
            platformObject.SetActive(true);
            if(platform.type == 0)
            {
                platformPos = Camera.main.ScreenToWorldPoint(new Vector3(2000, 445, 10));
            }
            else
            {
                platformPos = Camera.main.ScreenToWorldPoint(new Vector3(2000, 445, 10));
            }
            platformObject.transform.position = new Vector3(platform.pos + platformPos.x + 20, platformPos.y, 0);
            Debug.Log(platformObject.transform.position);
        }
        foreach (var enemy in pd.e_Data)
        {
            Debug.Log($"타입  {enemy.type}번 몬스터 생성");
            GameObject enemyObject = PoolManager.GetObject(2 + enemy.type);
            Vector3 enemPos = Camera.main.ScreenToWorldPoint(new Vector3(2000, 285 + 300 * enemy.floor, 0));
            enemyObject.transform.position = new Vector3(enemPos.x + enemy.x_pos + 20, enemPos.y, 0);
        }
    }

    
}
