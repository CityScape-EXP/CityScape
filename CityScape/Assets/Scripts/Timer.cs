using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject popupGameClear;
    public GameObject popupGameOver;

    [SerializeField]
    public Slider timerSlider;

    public float gameTime; //게임플레이시간 설정(초단위)
    private bool stopTimer;

    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime; //슬라이더가 나타내는 최대시간
        timerSlider.value = gameTime;
        StartTimer();
    }

    //
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
                ShowPopup();
            }

            if(stopTimer == false){ //slidervalue를 업뎃해야함
                if(timerSlider!=null){
                    timerSlider.value = sliderTimer;
                }
            }
            Debug.Log("Slider Timer: " + sliderTimer + " " + stopTimer + " " + timerSlider.value);

            //게임오버창이 활성화되면 타이머가 멈춤
            if(popupGameOver != null && popupGameOver.activeSelf){
                Debug.Log("게임오버");
                stopTimer = true;
            }
        }
    }

    void ShowPopup(){
        popupGameClear.SetActive(true);
    }
}
