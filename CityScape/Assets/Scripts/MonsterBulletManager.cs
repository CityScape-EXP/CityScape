using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBulletManager : MonoBehaviour
{
    public GameObject[] prefabs; // �ν����Ϳ��� �ʱ�ȭ
    public GameObject Monster;
    public int id;

    public Vector3 FixPos; // �Ѿ� ���� ��ġ ����

    public float BulletWaitTime;
    public float BulletSpawnTime;

    public bool isMonsterLive;
    

    private void Start()
    {
        isMonsterLive = true; // Monster�� ���� ���·� ����
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine() // �Ѿ� ���� ��ƾ
    {
        yield return new WaitForSeconds(BulletWaitTime);

        while (isMonsterLive)
        { // ���� ���� ��츸 �Ѿ� ����
            GameObject MonsterBullet = GameManager.instance.BulletPool.Get(1);
            MonsterBullet.transform.position = Monster.transform.position + FixPos;
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
        }
    }

    private void Update() // ���� ��� ���� üũ
    {
        if (Monster != null){
            Monster MonsterObj = Monster.GetComponent<Monster>();
            if (MonsterObj != null){
                isMonsterLive = MonsterObj.isLive; // Monster ������Ʈ�� isLive ���� ��������
                Debug.Log("MonsterObj.isLive : " + MonsterObj.isLive);
            }
        }
        Debug.Log("MBM Pos : " + Monster.transform.position);
    }
}
