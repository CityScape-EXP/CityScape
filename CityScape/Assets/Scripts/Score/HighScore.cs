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

        int stageNum = GameManager.instance.stageNum;
        
        if (stageNum >= 0 && stageNum < gameData.stageHighScore.Count)
        {
            Debug.Log(stageNum);
            int highScore = gameData.stageHighScore[stageNum];
            if (textComponent != null)
            {
                textComponent.text = "High Score: " + highScore.ToString();
            }
            else
            {
                Debug.LogWarning("HighScore Text 찾을 수 없음 " + gameObject.name);
            }
        }
        else
        {
            Debug.LogWarning("StageNum 오류: " + stageNum);
        }
        
    }
}