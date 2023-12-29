using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class StageStart : MonoBehaviour
{   
    //싱글톤 적용
    public static StageStart instance { get; set; }
    
    private void Awake(){
        /* 싱글톤 적용 */
        if (instance == null)
        {
            instance = this;

            // 과연 필수일까? 
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        
    }

    //public int StageNum; //선택한 스테이지 번호
    public string roadStage1 = "TestScene";
    public void StartStage1() 
    {

        DataManager.NowStage = Define.Stages.Stage1;
        Debug.Log("Get");

        GameManager.Sound.Play(Define.BGM.St_1);
        GameManager.instance.stageNum = 1;
        Time.timeScale = 1;
        GameManager.instance.stageTime = 0; //stageTime 초기화
        UIManager.pauseOnclicked = false; //static변수는 instance 안써도 됨
        MapBuilder.instance.Init_var();
        SceneManager.LoadScene("TestScene");

        //StartCoroutine(LoadMainMenuScene());     //다른 씬이 로드되는 시점 게임 오브젝트가 파괴되면 과연 코루틴은 작동을 할까요?
    }

    public void StartStage2(){
        DataManager.NowStage = Define.Stages.Stage2;

        SceneManager.LoadScene("Stage2Scene");
    }
    public void StartStage3(){
        DataManager.NowStage = Define.Stages.Stage3;

        SceneManager.LoadScene("Stage3Scene");
    }
}