using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public static bool isMenu = true;
    public static bool isGame = false;
    public string roadMainMenu = "MainPopup";
    [Header("settingCanvas")]
    [SerializeField] public GameObject settingCanvas;
    [Header("MainUI")]
    [SerializeField] private GameObject settingPanel;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(settingCanvas);
        }
        else
        {
            Destroy(gameObject);
            Destroy(settingCanvas);
        }
    }
    private void Start()
    {
        
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
        UIManager.instance.settingPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OffPausePanel()
    {
        UIManager.instance.settingPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void OnSettingPanel()
    {
        UIManager.instance.settingPanel.SetActive(true);
    }

    public void OffSettingPanel()
    {
        UIManager.instance.settingPanel.SetActive(false);
    }
    public void GoMenuButton()
    {
        StartCoroutine(LoadMainMenuScene());
        settingPanel.SetActive(false);
    }
    IEnumerator LoadMainMenuScene()
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roadMainMenu);

        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            isMenu = true;
            isGame = false;
            yield return null;
        }
    }
}
