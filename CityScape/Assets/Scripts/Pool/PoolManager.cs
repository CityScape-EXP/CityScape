using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �̱���
    public static PoolManager Instance;
    // ���� ��������� ��� objectPrefeb ����Ʈ
    [SerializeField] private List<GameObject> objectPrefebList = new List<GameObject>();
    private List<Queue<GameObject>> poolingObjectQueueList = new List<Queue<GameObject>>();


    // �̱��� ���� �� Initialize
    private void Start()
    {
        Instance = this;
        Initialize(10);
    }

    // ���ο� ���� ������Ʈ�� �����ϰ�, �ش� ������Ʈ�� ��ȯ�ϴ� �Լ�
    private GameObject createNewObject(int prefebID)
    {
        GameObject newObj = Instantiate(objectPrefebList[prefebID]);
        newObj.SetActive(false);
        return newObj; 
    }

    // ���� ���۽� ObjectPool �ʱ�ȭ (����Ʈ �ʱ�ȭ �� �� ť�� �ӽ� prefeb 10���� ����)
    private void Initialize(int count)
    {
        int prefebNum = objectPrefebList.Count;
        for(int i = 0; i < prefebNum; i++)
        {
            poolingObjectQueueList.Add(new Queue<GameObject>());
            for(int j = 0; j< prefebNum; j++)
            {
                poolingObjectQueueList[prefebNum].Enqueue(createNewObject(prefebNum));
            }
        }
    }    

    // GameObject�� Pool���� �������� �Լ�
    public static GameObject GetObject(int prefebID)
    {
        // Pool�� ���� GameObject�� ���� �� ���� Object return
        if (Instance.poolingObjectQueueList[prefebID].Count > 0)
        {
            GameObject obj = Instance.poolingObjectQueueList[prefebID].Dequeue();
            obj.transform.SetParent(null);
            obj.SetActive(true);
            return obj;
        }
        // Pool�� ���� GameObject�� ���� ��.. ���� Pool�� ���� �� return
        else
        {
            GameObject newObj = Instance.createNewObject(prefebID);
            newObj.transform.SetParent(null);
            newObj.SetActive(true);
            return newObj;
        }
    }

    // GameObject�� �ش��ϴ� prefeb ID�� ������ Pool�� �����Ѵ�
    public static void ReturnObject(GameObject obj, int prefebID)
    {
        obj.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueList[prefebID].Enqueue(obj);
    }


}
