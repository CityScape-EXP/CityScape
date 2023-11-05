using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; //�̱��� ������ // �ܺο��� GameManager�� �����Ҷ� �̰� ����ٰ� ����
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject audioPanel;
    
    [SerializeField] GameData gameData;
    [SerializeField] PlayerData playerData;

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
        gameData = DataManager.GetGameData();
        playerData = DataManager.GetPlayerData();
    }

    private void Update()
    {
        
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
