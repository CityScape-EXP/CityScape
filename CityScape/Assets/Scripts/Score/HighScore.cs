using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();

        if (textComponent != null){
            textComponent.text = "High Score: " + Score.highScore.ToString();        
        }
        else{
            Debug.LogWarning("high score 텍스트 찾을 수 없음. " + gameObject.name);
        }

    }
}