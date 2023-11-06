using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject audioPanel;
    
    [SerializeField] GameData gameData;
    [SerializeField] PlayerData playerData;

    public PoolManager pool;
    public Player player;
    public float surviveTime;
    public bool isGameover;


    private void Start()
    {
        surviveTime = 0;
        isGameover = false;
        gameData = DataManager.GetGameData();
        playerData = DataManager.GetPlayerData();
    }

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
    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
        }
    }

    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void ShowAudioPanel()
    {
        audioPanel.SetActive(true);
    }

}
