using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour //UI상 텍스트에 최고 스코어를 나타내기 위한 스크립트
{
    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = DataManager.instance.GetGameData();

        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        
        if(GameManager.instance.stageNum == 1){ //스테이지 1
            if (textComponent != null){
                textComponent.text = "High Score : " + gameData.stageHighScore[0].ToString();        
            }
            else{
                Debug.LogWarning("HS 텍스트 찾을 수 없음. " + gameObject.name);
            }
        }
        
        else if(GameManager.instance.stageNum == 2){ //스테이지 2
            if (textComponent != null){
                textComponent.text = "High Score : " + gameData.stageHighScore[1].ToString();        
            }
            else{
                Debug.LogWarning("HS 텍스트 찾을 수 없음. " + gameObject.name);
            }
        }

        else{ //스테이지 3
            if (textComponent != null){
                textComponent.text = "High Score : " + gameData.stageHighScore[2].ToString();        
            }
            else{
                Debug.LogWarning("HS 텍스트 찾을 수 없음. " + gameObject.name);
            }
        }
        
    }
}