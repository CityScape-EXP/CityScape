using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 싱글톤
    public static PoolManager Instance;
    // 게임 프리펩들을 담는 objectPrefeb 리스트
    [SerializeField] private List<GameObject> objectPrefebList = new List<GameObject>();
    // 프리펩 수만큼의 Queue<GameObject> 생성
    private List<Queue<GameObject>> poolingObjectQueueList = new List<Queue<GameObject>>();

    private void Awake()
    {
         Instance = this;
    }

    // Initialize
    private void Start()
    {
        for(int i = 0; i < objectPrefebList.Count; i++)
        {
            poolingObjectQueueList.Add(new Queue<GameObject>());
        }
        Initialize(10);
    }

    // 새로운 게임 오브젝트를 생성하고, 해당 오브젝트를 반환하는 함수
    private GameObject createNewObject(int prefebID)
    {
        GameObject newObj = Instantiate(objectPrefebList[prefebID]);
        newObj.transform.SetParent(Instance.transform);
        newObj.SetActive(false);
        return newObj; 
    }

    // 게임 시작시 ObjectPool 초기화 (리스트 초기화 및 각 큐에 임시 prefeb 10개씩 생성)
    private void Initialize(int count)
    {
        int prefebNum = objectPrefebList.Count;
        for(int i = 0; i < prefebNum; i++)
        {
            for(int j = 0; j< count; j++)
            {
                poolingObjectQueueList[i].Enqueue(createNewObject(i));
            }
        }
    }    

    // GameObject를 Pool에서 가져가는 함수
    public static GameObject GetObject(int prefebID)
    {
        //Debug.Log($"{prefebID}번 오브젝트 생성");
        // Pool에 남는 GameObject가 있을 때 남는 Object return
        if (Instance.poolingObjectQueueList[prefebID].Count > 0)
        {
            GameObject obj = Instance.poolingObjectQueueList[prefebID].Dequeue();
            //obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
        // Pool에 남는 GameObject가 없을 떄.. 새로 Pool에 생성 후 return
        else
        {
            GameObject newObj = Instance.createNewObject(prefebID);
            //newObj.transform.SetParent(null);
            newObj.SetActive(true);
            return newObj;
        }
    }

    // GameObject와 해당하는 prefeb ID를 가지고 Pool에 리턴한다
    public static void ReturnObject(GameObject obj, int prefebID)
    {
        obj.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueList[prefebID].Enqueue(obj);
    }
}
