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
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roadMainMenu);

        // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
        while (!asyncLoad.isDone)
        {
            yield return null;
            isMenu = true;
            isGame = false;
        }
    }
}
