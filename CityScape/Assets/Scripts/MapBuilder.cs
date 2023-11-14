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
    // Prefeb ����Ʈ
    public List<PrefebElement> prefebElements;



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
        // platform�� Prefeb ������ �����´�
        GameObject platformPrefab = GetPrefeb("Platform");
        foreach (var platform in pd.p_Data)
        {
            for(int i = 0; i< platform.width; i++)
            {
                // platform Ŭ���� �����͸� �̿��Ͽ� prefeb Instantiate
                Instantiate(platformPrefab, new Vector3(platform.pos + 20 + i, platform.floor * 1.5f, 0), Quaternion.identity);
            }
        }
        foreach (var enemy in pd.e_Data)
        {
            // Type ��ȣ�� ��ġ�ϴ� Enemy prefeb ������
            GameObject enemyPrefeb = GetPrefeb("Enemy" + enemy.type.ToString());
            Instantiate(enemyPrefeb, new Vector3(enemy.x_pos + 20, enemy.y_pos, 0), Quaternion.identity);
        }
    }
}
