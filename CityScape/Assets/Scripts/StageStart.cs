using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
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
        Debug.Log("Get");

        GameManager.Sound.Play(Define.BGM.St_1);
        SceneManager.LoadScene("TestScene");

        //StartCoroutine(LoadMainMenuScene());     //다른 씬이 로드되는 시점 게임 오브젝트가 파괴되면 과연 코루틴은 작동을 할까요?
    }
    IEnumerator LoadMainMenuScene()
    {
        // AsyncOperation을 통해 Scene Load 정도를 알 수 있다.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roadStage1);
        // Scene을 불러오는 것이 완료되면, AsyncOperation은 isDone 상태가 된다.
        while (!asyncLoad.isDone)
        {
            UIManager.isMenu = false;
            UIManager.isGame = true;
            GameManager.instance.stageNum = 1;
            Time.timeScale = 1;
            GameManager.instance.stageTime = 0; //stageTime 초기화
            UIManager.pauseOnclicked = false; //static변수는 instance 안써도 됨
            Debug.Log(UIManager.isMenu);
            yield return null;
        }
    }
    public void StartStage2(){
        SceneManager.LoadScene("Stage2Scene");
    }
    public void StartStage3(){
        SceneManager.LoadScene("Stage3Scene");
    }
}