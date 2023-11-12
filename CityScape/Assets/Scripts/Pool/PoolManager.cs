using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // �̱���
    public static PoolManager Instance;
    // ���� ��������� ��� objectPrefeb ����Ʈ
    [SerializeField] private List<GameObject> objectPrefebList = new List<GameObject>();
    // ������ ����ŭ�� Queue<GameObject> ����
    private List<Queue<GameObject>> poolingObjectQueueList = new List<Queue<GameObject>>();

    private void Awake()
    {
        // ������Ʈ Ǯ��
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize
        for (int i = 0; i < objectPrefebList.Count; i++)
        {
            poolingObjectQueueList.Add(new Queue<GameObject>());
        }
        Initialize(10);

    }

    
    // Initialize
    private void Start()
    {
        // Awake�� �ű�...
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
            for(int j = 0; j< count; j++)
            {
                poolingObjectQueueList[i].Enqueue(createNewObject(i));
            }
        }
    }    

    // GameObject�� Pool���� �������� �Լ�
    public static GameObject GetObject(int prefebID)
    {
        Debug.Log($"{prefebID}�� ������Ʈ ����");
        // Pool�� ���� GameObject�� ���� �� ���� Object return
        if (Instance.poolingObjectQueueList[prefebID].Count > 0)
        {
            GameObject obj = Instance.poolingObjectQueueList[prefebID].Dequeue();
            obj.transform.SetParent(null); // ���߿� �� �κ� ��������.
            obj.SetActive(true);
            return obj;
        }
        // Pool�� ���� GameObject�� ���� ��.. ���� Pool�� ���� �� return
        else
        {
            GameObject newObj = Instance.createNewObject(prefebID);
            newObj.transform.SetParent(null); // ���߿� �� �κ� ��������.
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
