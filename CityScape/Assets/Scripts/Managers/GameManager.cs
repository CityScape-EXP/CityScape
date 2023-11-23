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
    [SerializeField] public GameData gameData;
    [SerializeField] public UpgradeData upgradeData;

    public Player player;
    public float surviveTime; //초기부터진행시간
    public bool isGameover;
    public DataManager dm;

    void Awake()
    {
        dm = GetComponent<DataManager>();
        gameData = dm.GetGameData();
        upgradeData = dm.GetUpgradeData();
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
    }
    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
        }
    }
}
