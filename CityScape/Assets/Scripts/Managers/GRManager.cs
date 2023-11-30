using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GRManager : MonoBehaviour //GameResult(Clear, Over)매니저 스크립트
{    
    private int HighScore1;
    public GameData gameData;
    public string roadMainMenu = "MainPopup";

    [Header("GameClear")]
    [SerializeField] private GameObject popupGameClear;
    public Slider timerSlider;
    public float gameTime; //게임플레이시간 설정(초단위)
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
            GRManager.instance.popupGameOver.SetActive(true);
            //Debug.Log("게임오버");
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

    public void GoMenuButton() //메인메뉴로(mainpopup)
    {
        MainMenu.isStart = false;
        if (popupGameClear.activeSelf && !popupGameOver.activeSelf){
            popupGameClear.SetActive(false); //gameclear 팝업 닫기
        }
        else{
            popupGameOver.SetActive(false); //gameover 팝업 닫기
        }
        SceneManager.LoadScene("MainPopup");
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
            if(popupGameClear.activeSelf && !popupGameOver.activeSelf){
            popupGameClear.SetActive(false);
            }
            else{
            popupGameOver.SetActive(false);
            }
            MapBuilder.instance.Init_var();
            yield return null;
        }
        Debug.Log("다음 씬으로 로드 완료");
        
    }

}
