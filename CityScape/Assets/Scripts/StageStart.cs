using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    public string roadStage1 = "TestScene";
    public void StartStage1()
    {
        
        StartCoroutine(LoadMainMenuScene());
    }
    IEnumerator LoadMainMenuScene()
    {
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(roadStage1);
        // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
        while (!asyncLoad.isDone)
        {
            UIManager.isMenu = false;
            UIManager.isGame = true;
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