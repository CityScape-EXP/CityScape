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
    // 패턴 누적수 (초, 중, 후반 나누는 분기점)
    public int acmPattern = 0;
    int nowPhase = 0;
    float patternStartTime = 0f;
    float nowPatternTime = 0f;
    // Prefeb 리스트
    public List<PrefebElement> prefebElements;



    private void Update()
    {
        // TestScene(인게임)일 경우에만 MapBuilder 실행한다
        if (!(SceneManager.GetActiveScene().name == "TestScene"))
            return;

        // 패턴에 할당된 시간이 지났다면
        if(Time.time - patternStartTime > nowPatternTime)
        {
            //초반, 중반, 후반 분기 결정
            // 5개, 8개, 6개로 가정
            if (acmPattern < 5) nowPhase = 0;
            else if (acmPattern < 13) nowPhase = 1;
            else if (acmPattern < 19) nowPhase = 2;
            else Debug.Log("스테이지 클리어");

            int patternNum = Random.Range(0, 4);
            Debug.Log($"패턴 {patternNum}번 실행!");
            PatternData nowPattern = GameManager.instance.dm.GetPatternData(0, nowPhase, patternNum);
            DrawPattern(nowPattern);

            nowPatternTime = nowPattern.patternTime;
            patternStartTime = Time.time;
            acmPattern++;
        }
    }

    GameObject GetPrefeb(string s)
    {
        PrefebElement prefebElement = prefebElements.Find(le => le.m_Name == s);
        if (prefebElement != null)
            return prefebElement.m_Prefeb;
        else
            return null;
    }

    private void DrawPattern(PatternData pd)
    {
        // platform의 Prefeb 정보를 가져온다
        GameObject platformPrefab = GetPrefeb("Platform");
        foreach (var platform in pd.p_Data)
        {
            for(int i = 0; i< platform.width; i++)
            {
                // platform 클래스 데이터를 이용하여 prefeb Instantiate
                Instantiate(platformPrefab, new Vector3(platform.pos + 20 + i, platform.floor * 1.5f, 0), Quaternion.identity);
            }
        }
        foreach (var enemy in pd.e_Data)
        {
            // Type 번호에 일치하는 Enemy prefeb 가져옴
            GameObject enemyPrefeb = GetPrefeb("Enemy" + enemy.type.ToString());
            Instantiate(enemyPrefeb, new Vector3(enemy.x_pos + 20, enemy.y_pos, 0), Quaternion.identity);
        }
    }
}
