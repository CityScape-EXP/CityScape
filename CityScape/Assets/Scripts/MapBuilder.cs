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
    public int acmPattern = 0;
    int nowPhase = 0;
    float patternStartTime = 0f;
    float nowPatternTime = 0f;



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
            Debug.Log($"���� {patternNum}�� ����!");
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
