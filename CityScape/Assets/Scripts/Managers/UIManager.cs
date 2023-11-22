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
    [SerializeField] public GameObject settingPanel;
    
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
        SceneManager.LoadScene("MainPopup");
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        settingPanel.SetActive(false);
        MapBuilder.instance.Init_var();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 Scene을 다시 로드시키는 문장
    }
}
