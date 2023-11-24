using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public static bool isMenu = true;
    public static bool isGame = false;
    [Header("settingCanvas")]
    [SerializeField] public GameObject settingCanvas;
    [Header("MainUI")]
    [SerializeField] public GameObject settingPanel;
    public GameObject MainMenu;
    
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
        isMenu = true;
        isGame = false;
        Time.timeScale = 1f;
        settingPanel.SetActive(false);
        //MainMenu.instance.mainMenu.SetActive(true);
        SceneManager.LoadScene("MainPopup");
    }

    public void OnRestartButton()
    {
        StartCoroutine(RestartStage());
    }

    IEnumerator RestartStage()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("다음 씬으로 로드 완료");
        settingPanel.SetActive(false);
        Time.timeScale = 1f;
        MapBuilder.instance.Init_var();
    }
}
