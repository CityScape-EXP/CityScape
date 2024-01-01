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
        GameManager.instance.stageNum = 1;
        GameManager.Sound.Play(Define.BGM.St_1);
        StartStage();
    }

    public void StartStage2(){
        DataManager.NowStage = Define.Stages.Stage2;
        GameManager.instance.stageNum = 2;
        GameManager.Sound.Play(Define.BGM.St_2);
        StartStage();
    }
    public void StartStage3(){
        DataManager.NowStage = Define.Stages.Stage3;
        GameManager.instance.stageNum = 3;
        GameManager.Sound.Play(Define.BGM.St_3);
        StartStage();
    }

    public void StartStage()
    { 
        Debug.Log("Get");
        Time.timeScale = 1;
        GameManager.instance.stageTime = 0; //stageTime 초기화
        UIManager.pauseOnclicked = false; //static변수는 instance 안써도 됨
        MapBuilder.instance.Init_var();
        SceneManager.LoadScene("TestScene");
    }
}