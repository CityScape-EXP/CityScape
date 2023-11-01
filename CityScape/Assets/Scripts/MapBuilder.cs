using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefebElement
{
    public string m_Name;
    public GameObject m_Prefeb;
}

[System.Serializable]
public class LevelData
{
    public List<PatternData> earlyPatterns;
    public List<PatternData> middlePatterns;
    public List<PatternData> latePatterns;
}

public class MapBuilder : MonoBehaviour
{
    public LevelData levelData;
    // ���� ������ (��, ��, �Ĺ� ������ �б���)
    public int acmPattern = 0;
    // Prefeb ����Ʈ
    public List<PrefebElement> prefebElements;

    PatternData exPattern;

    GameObject GetPrefeb(string s)
    {
        PrefebElement prefebElement = prefebElements.Find(le => le.m_Name == s);
        if (prefebElement != null)
            return prefebElement.m_Prefeb;
        else
            return null;
    }

    private void Start()
    {
        exPattern = DataManager.GetPatternData(0, 0, 0);
        DrawPattern(exPattern);
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
                Instantiate(platformPrefab, new Vector3(platform.pos + i, platform.floor * 1.5f, 0), Quaternion.identity);
            }
        }
        foreach (var enemy in pd.e_Data)
        {
            // Type ��ȣ�� ��ġ�ϴ� Enemy prefeb ������
            GameObject enemyPrefeb = GetPrefeb("Enemy" + enemy.type.ToString());
            Instantiate(enemyPrefeb, new Vector3(enemy.x_pos, enemy.y_pos, 0), Quaternion.identity);
        }
    }
}
