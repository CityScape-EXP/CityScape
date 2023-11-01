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
        GameObject prefab = GetPrefeb("Platform");
        Instantiate(prefab, new Vector3(6, 1.5f, 0), Quaternion.identity);
        Instantiate(prefab, new Vector3(10, 3f, 0), Quaternion.identity);
        Instantiate(prefab, new Vector3(19, 1.5f, 0), Quaternion.identity);
    }
}
