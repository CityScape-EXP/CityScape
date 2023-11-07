using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public static bool isMenu = true;
    public static bool isGame = false;
    public string roadMainMenu = "MainPopup";
    [Header("MainUI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject offSettingPanelButton;
    [SerializeField] private GameObject creditPanelButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject settingText;

    [Header("GameUI")]
    [SerializeField] private GameObject PauseText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject goToMenuButton;
    
    
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
    public void OnExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void OnPausePanel()
    {
        settingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OffPausePanel()
    {
        settingPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void GoMenuButton()
    {
        StartCoroutine(LoadMainMenuScene());
    }
    IEnumerator LoadMainMenuScene()
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roadMainMenu);

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            yield return null;
            isMenu = true;
            isGame = false;
        }
    }
}
