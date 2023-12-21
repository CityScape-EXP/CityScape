using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    /// <summary>
    /// Game Object의 자식 중 T 컴포넌트를 가진 자식 얻기
    /// </summary>
    /// <param name="go"> 부모 객체 </param>
    /// <param name="name">자식의 이름</param>
    /// <param name="recursive">재귀적 탐색 여부</param>
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform child = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    T component = child.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T child in go.GetComponentsInChildren<T>())
                if (string.IsNullOrEmpty(name) || child.name == name)
                    return child;
        }

        return null;
    }
}
