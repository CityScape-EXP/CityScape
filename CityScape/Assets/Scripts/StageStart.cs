using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    public void StartStage1()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void StartStage2(){
        SceneManager.LoadScene("Stage2Scene");
    }
    public void StartStage3(){
        SceneManager.LoadScene("Stage3Scene");
    }
}