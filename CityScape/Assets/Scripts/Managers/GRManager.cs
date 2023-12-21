using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GRManager : MonoBehaviour //GameResult(Clear, Over)매니저 스크립트
{    
    //private int HighScore1;
    public GameData gameData;
    public string roadMainMenu = "MainPopup";

    [Header("GameClear")]
    [SerializeField] private GameObject popupGameClear;
    public Slider timerSlider;
    public float gameTime; //슬라이더에 사용할 게임플레이시간(초단위)
    private bool stopTimer;
    [Header("GameOver")]
    [SerializeField] private GameObject popupGameOver;

    //싱글톤 적용
    public static GRManager instance;
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        //HighScore1 = DataManager.instance.GameData.stageHighScore[0];
        //DataManager.instance.GameData.isStageOpen -> 클리어시
        //DataManager.instance.GameData.stageHighScore -> 둘 다 사용
        gameData = DataManager.instance.GetGameData();

        stopTimer = false;
        timerSlider.maxValue = gameTime; //슬라이더가 나타내는 최대시간
        timerSlider.value = gameTime;       
        StartTimer();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.instance.isLive == false){
            GRManager.instance.popupGameOver.SetActive(true); //게임오버
            //스테이지별 HighScore 업뎃
            if(GameManager.instance.stageNum == 1){ //스테이지 1
                if(FinalScore.finalScore > gameData.stageHighScore[0]){
                    gameData.stageHighScore[0] = FinalScore.finalScore;
                }
            }
            else if(GameManager.instance.stageNum == 2){ //스테이지 2
                if(FinalScore.finalScore > gameData.stageHighScore[1]){
                    gameData.stageHighScore[1] = FinalScore.finalScore;
                }
            }
            else{ //스테이지 3
                if(FinalScore.finalScore > gameData.stageHighScore[2]){
                    gameData.stageHighScore[2] = FinalScore.finalScore;
                }
            }
            Time.timeScale = 0f; //인게임일시정지
        }
    }

    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    IEnumerator StartTheTimerTicker(){
        float sliderTimer = gameTime;

        while(stopTimer == false && sliderTimer > 0){
            sliderTimer -= Time.deltaTime; //점점 줄어듦
            yield return null;

            if(sliderTimer <= 0){
                stopTimer = true;
                GRManager.instance.popupGameClear.SetActive(true);
                Time.timeScale = 0f; //인게임일시정지
                gameData.isStageOpen[1] = true; //다음 스테이지 열기
            }

            if(stopTimer == false){ //slidervalue를 업뎃해야함
                if(timerSlider!=null){
                    timerSlider.value = sliderTimer;
                }
            }
            //Debug.Log("Slider Timer: " + sliderTimer + " " + stopTimer + " " + timerSlider.value); //남은 시간 확인용

            //게임오버창이 활성화되면 타이머가 멈춤
            if(popupGameOver != null && popupGameOver.activeSelf){
                Debug.Log("게임오버");
                stopTimer = true;
                Time.timeScale = 0f; //인게임일시정지
            }
        }
    }

    public void GoMenuButton() //메인메뉴로(mainpopup)   // init_var 문제로 async로 변경
    {
        StartCoroutine(GoMenu());
    }
    IEnumerator GoMenu()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainPopup");
        while (!asyncLoad.isDone)
        {
            MainMenu.isStart = false;
            if (popupGameClear.activeSelf && !popupGameOver.activeSelf)
            {
                popupGameClear.SetActive(false); //gameclear 팝업 닫기
            }
            else
            {
                popupGameOver.SetActive(false); //gameover 팝업 닫기
            }
            MapBuilder.instance.Init_var();
            yield return null;
        }
    }

    public void OnRestartButton() //다시시작
    {
        StartCoroutine(RestartStage());
        Debug.Log("1");
        
    }

    IEnumerator RestartStage()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while(!asyncLoad.isDone)
        {
            Time.timeScale = 1f; 
            if(popupGameClear.activeSelf && !popupGameOver.activeSelf){ //게임클리어 시
            GameManager.instance.stageTime = 0; //시간 초기화
            UIManager.pauseOnclicked = false;
            popupGameClear.SetActive(false); //게임클리어창 닫기
            }
            else{ //게임오버 시
            GameManager.instance.stageTime = 0; //시간 초기화
            UIManager.pauseOnclicked = false;
            popupGameOver.SetActive(false); //게임오버창 닫기
            }
            MapBuilder.instance.Init_var();
            yield return null;
        }
        Debug.Log("다음 씬으로 로드 완료");
    }

}
