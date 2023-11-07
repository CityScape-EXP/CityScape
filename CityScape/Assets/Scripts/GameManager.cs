using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;    
    [Header("Data")]
    [SerializeField] GameData gameData;
    [SerializeField] PlayerData playerData;

    public BulletPool BulletPool;
    public MonsterPool MonsterPool;
    public Player player;
    public float surviveTime;
    public bool isGameover;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        surviveTime = 0;
        isGameover = false;
        gameData = DataManager.GetGameData();
        playerData = DataManager.GetPlayerData();
    }
    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
        }
    }
}
