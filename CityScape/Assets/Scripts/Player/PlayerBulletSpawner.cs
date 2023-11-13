using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletSpawner : MonoBehaviour
{
    /* �Ѿ� ���� */
    public float WaitForFirstSpawn;
    public float BulletSpawnTime;
    public Vector3 FixPos;

    /* �÷��̾� ���� */
    private GameObject Player;
    public bool isPlayerLive;

    /* �ʱ�ȭ */
    private void Awake()
    {
        Player = gameObject;
    }

    /* �÷��̾ ���� ���·� �����ϰ�, �Ѿ� ���� ��ƾ�� ������ */
    private void Start()
    {
        isPlayerLive = true;

        StartCoroutine(SpawnRoutine());

        // ������ ���� �Ѿ� ��Ÿ�� ����
        int asLevel = GameManager.instance.upgradeData.asLevel;
        BulletSpawnTime = BulletSpawnTime / (1.00f + (asLevel - 1) * 0.25f);
    }

    /* �Ѿ� ���� ��ƾ �Լ� */
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(WaitForFirstSpawn);
        // WaitForFirstSpawn �� ���� �Ѿ� ���� ����

        while (true)
        {
            GameObject obj = PoolManager.GetObject(0);
            // Pool�� 0��° ������(�Ѿ�)�� ������. ���߿� �ش� �κ� ���� ���ɼ� ����
            obj.transform.position = Player.transform.position + FixPos;
            
            yield return new WaitForSeconds(BulletSpawnTime); // BulletSpawnTime �� ��ŭ ��� �� ����
        }
    }

    /* �÷��̾� ��� ���� üũ */
    private void Update()
    {
        if (Player != null)
        {
            Player PlayerObj = Player.GetComponent<Player>();

            if (PlayerObj != null)
            {
                isPlayerLive = PlayerObj.isLive; // Monster ������Ʈ�� isLive ���� ��������
            }
        }
    }
}
