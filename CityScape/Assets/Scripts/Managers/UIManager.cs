using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    //싱글톤??

    static UIManager _instance = null;

    public static UIManager instance { get { Init(); return _instance; } }

    public static bool pauseOnclicked = false;
    public static bool isStart = true;
    [Header("settingCanvas")]
    [SerializeField] public GameObject settingCanvas;
    [Header("MainUI")]
    [SerializeField] public GameObject settingPanel;

    GameObject _root;
    public static GameObject Root
    {
        get
        {
            if(instance._root == null)
            {

                GameObject root = GameObject.Find("UI_Root");
                if (root == null)
                {
                    root = new GameObject { name = "UI_Root" };
                }

                _instance._root = root;
                return _instance._root;
            }
            else
            {
                return _instance._root;
            }
        }
    }

    static void Init()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<UIManager>();

            if(_instance == null)
            {
                GameObject uim = new GameObject("UIManager");
                _instance = uim.AddComponent<UIManager>();
            }

            DontDestroyOnLoad(_instance.gameObject);

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
        UIManager._instance.settingPanel.SetActive(true);
        Time.timeScale = 0f;
        pauseOnclicked = true;
    }

    public void OffPausePanel()
    {
        UIManager._instance.settingPanel.SetActive(false);
        Time.timeScale = 1f;
        pauseOnclicked = false;
    }
    public void OnSettingPanel()
    {
        UIManager._instance.settingPanel.SetActive(true);
    }

    public void OffSettingPanel()
    {
        UIManager._instance.settingPanel.SetActive(false);
    }
    public void GoMenuButton()  // init_var작동문제로 async로 변경
    {
        UIManager.isStart = false;
        SceneManager.LoadScene("MainPopUp");
    }
    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        MapBuilder.instance.Init_var();
        GameManager.instance.stageTime = 0; //시간 초기화
        pauseOnclicked = false;
        SceneManager.LoadScene("TestScene");
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
        GameManager.instance.stageTime = 0; //시간 초기화
        pauseOnclicked = false;
    }


    static public GameObject LoadUI(Define.UI_Type uI_Type)
    {
        GameObject ui;
        try
        {
            ui = Instantiate(Resources.Load<GameObject>($"Prefabs/UIs/{uI_Type.ToString()}"));
            ui.transform.parent = Root.transform;
            return ui;
        }
        catch
        {
            Debug.LogError($"UI Load 문제 발생 Prefabs/UIs/{uI_Type.ToString()} 확인 바람");
            return null;
        }




    }
    static public GameObject LoadUI(string uI_Type)
    {
        GameObject ui;
        try
        {
            ui = Instantiate(Resources.Load<GameObject>($"Prefabs/UIs/{uI_Type}"));
            ui.transform.parent = Root.transform;
            return ui;
        }
        catch
        {
            Debug.LogError($"UI Load 문제 발생 Prefabs/UIs/{uI_Type} 확인 바람");
            return null;
        }
    }
}
