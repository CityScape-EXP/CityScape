using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject[] prefabs; // 인스펙터에서 초기화
    //public GameObject Player;
    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; // 풀 생성

        for (int index = 0; index < pools.Length; index++)
            pools[index] = new List<GameObject>(); // 풀 초기화
    }

    public GameObject Get(int ObjectType)
    {
        GameObject select = null; // select: 받아올 오브젝트

        foreach (GameObject item in pools[ObjectType]) // pools 안의 것들 순서대로 가져옴
        {
            if (!item.activeSelf) // 작동하고 있지 않다면
            {
                select = item;
                select.SetActive(true); // 작동시키기
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[ObjectType]); // 프리펩 생성
            pools[ObjectType].Add(select); // 풀 추가
        }

        return select;
    }

    public void Clear(int ObjectType)
    {
        foreach (GameObject item in pools[ObjectType])
            item.SetActive(false);
    }

    public void ClearAll()
    {
        for (int index = 0; index < pools.Length; index++)
            foreach (GameObject item in pools[index])
                item.SetActive(false);
    }
    
}