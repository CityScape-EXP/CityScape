using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance = null;    //public 변수는 금기랍니다. 어디서 얼마나 불렸는지 알 수 없어요~

    #region 싱글톤
    /// <summary>
    /// 싱글톤은 무조껀 이렇게 3개가 다 있어야 싱글톤입니다. 
    /// </summary>
    static GameManager _instance = null;    //유일한 (또는 상수개의) static private 변수
    public static GameManager instance { get { Init();  return _instance; } }    //public static get함수 
    GameManager() { }   //!!!! private한 생성자

    #endregion 싱글톤


    [Header("Data")]
    [SerializeField] public GameData gameData;
    [SerializeField] public UpgradeData upgradeData;

    public Player player;
    public float surviveTime; //초기부터진행시간
    public float stageTime; //스테이지시작 후부터 진행시간
    public int stageNum; //선택한 스테이지의 번호
    public bool isGameover;
    public DataManager dm;


    SoundManager soundManager = new SoundManager();
    public static SoundManager Sound { get { return instance.soundManager; } } 

    static void Init()
    {

        if (_instance == null)
        {
            GameObject gm = GameObject.Find("GameManager");
            if (gm == null)
            {

                gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
                gm.AddComponent<DataManager>();
                gm.AddComponent<MapBuilder>();

                _instance.dm = _instance.GetComponent<DataManager>();

                _instance.gameData = _instance.dm.GetGameData();
                _instance.upgradeData = _instance.dm.GetUpgradeData();
                _instance.soundManager.Init();

                DontDestroyOnLoad(gm);


            }
            else
            {
                _instance = gm.GetComponent<GameManager>();
                _instance.dm = _instance.GetComponent<DataManager>();
                _instance.gameData = _instance.dm.GetGameData();
                _instance.upgradeData = _instance.dm.GetUpgradeData();
                _instance.soundManager.Init();

                DontDestroyOnLoad(gm);
            }

        }
        
    }

    void Awake()
    {
        Init();
        /*
        if (_instance == null)
        {
            GameObject gm = GameObject.Find("GameManager");
            if (gm == null)
            {
                gm = new GameObject("GameManager");
                gm.AddComponent<GameManager>();
                gm.AddComponent<DataManager>();
                gm.AddComponent<MapBuilder>();

                _instance.dm = _instance.GetComponent<DataManager>();

                _instance.gameData = _instance.dm.GetGameData();
                _instance.upgradeData = _instance.dm.GetUpgradeData();
                DontDestroyOnLoad(gm);

            }
            else
            {
                _instance = gm.GetComponent<GameManager>();
                _instance.dm = _instance.GetComponent<DataManager>();
                _instance.gameData = _instance.dm.GetGameData();
                _instance.upgradeData = _instance.dm.GetUpgradeData();
                DontDestroyOnLoad(gm);
            }

        }
        */
        /*
        dm = GetComponent<DataManager>();
        gameData = dm.GetGameData();
        upgradeData = dm.GetUpgradeData();
        if (instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */

    }

    private void Start()
    {
        surviveTime = 0;
        stageTime = 0;
        isGameover = false;
        UIManager.pauseOnclicked = true;
    }

    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
        }

        if(!UIManager.pauseOnclicked){

            stageTime += Time.deltaTime;
        }
    }
    
}
