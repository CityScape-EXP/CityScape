using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefebElement
{
    public string m_Name;
    public GameObject m_Prefeb;
}

public class MapBuilder : MonoBehaviour
{
    public List<PrefebElement> prefebElements;
    public int acmPattern = 0;

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
                Instantiate(platformPrefab, new Vector3(platform.pos + i, platform.floor * 1.5f, 0), Quaternion.identity);
            }
        }
        foreach (var enemy in pd.e_Data)
        {
            // Type 번호에 일치하는 Enemy prefeb 가져옴
            GameObject enemyPrefeb = GetPrefeb("Enemy" + enemy.type.ToString());
            Instantiate(enemyPrefeb, new Vector3(enemy.x_pos, enemy.y_pos, 0), Quaternion.identity);
        }
    }
}
