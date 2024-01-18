using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    // StageLock을 적용시켜줘야 하는 경우 -> 화면이 업데이트가 될 때마다? -> OnEnable
    public GameObject[] StageButtons;

    private void Update()
    {
        bool stage2Opened = DataManager.MainGameData.isStageOpen[1];
        Debug.Log(stage2Opened);

        for(int i = 0; i < StageButtons.Length; i++)
        {
            UI_EventHandler StageStartButton = StageButtons[i].GetComponent<UI_EventHandler>();
            if(StageStartButton == null)
            {
                Debug.Log("UI_EventHandler Component가 없습니다");
            }
            else
            {
                bool isOpened = DataManager.MainGameData.isStageOpen[i];
                if(isOpened) // Opened
                {
                    //Color, Lock Image, Button Active
                    StageButtons[i].GetComponent<Image>().color = Color.white;
                    StageButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                    StageStartButton.enabled = true;
                }
                else
                {
                    StageButtons[i].GetComponent<Image>().color = Color.gray;
                    StageButtons[i].transform.GetChild(0).gameObject.SetActive(true);
                    StageStartButton.enabled = false;
                }
            }
        }
    }
}
