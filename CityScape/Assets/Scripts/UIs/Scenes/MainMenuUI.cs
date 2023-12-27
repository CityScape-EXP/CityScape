using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : UI_Base , IPointerClickHandler
{
    enum Texts
    {
        Stage1_HighScore, 
        Stage2_HighScore,
        Stage3_HighScore
    }
    enum Buttons
    {
        Stage1_Button,
        Stage2_Button,
        Stage3_Button,
        SettingButton,
        ReinforceButton,
    }
    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        Get<TMP_Text>((int)Texts.Stage1_HighScore).text = DataManager.MainGameData.stageHighScore[(int)Define.Stages.Stage1].ToString();
        Get<TMP_Text>((int)Texts.Stage2_HighScore).text = DataManager.MainGameData.stageHighScore[(int)Define.Stages.Stage2].ToString();
        Get<TMP_Text>((int)Texts.Stage3_HighScore).text = DataManager.MainGameData.stageHighScore[(int)Define.Stages.Stage3].ToString();


        BindEvent(Get<Button>((int)Buttons.Stage1_Button).gameObject, Stage1Btn);
        BindEvent(Get<Button>((int)Buttons.Stage2_Button).gameObject, Stage2Btn);
        BindEvent(Get<Button>((int)Buttons.Stage3_Button).gameObject, Stage3Btn);
        BindEvent(Get<Button>((int)Buttons.SettingButton).gameObject, SettingBtn);
        BindEvent(Get<Button>((int)Buttons.ReinforceButton).gameObject, ReinforceBtn);

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Sound.Play(Define.SFX.UI_touch_1128);
    }
    void Stage1Btn(PointerEventData evt)
    {
        StageStart.instance.StartStage1();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
    void Stage2Btn(PointerEventData evt)
    {
        StageStart.instance.StartStage2();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
    void Stage3Btn(PointerEventData evt)
    {
        StageStart.instance.StartStage3();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
    void SettingBtn(PointerEventData evt)
    {
        UIManager.LoadUI(Define.UI_Type.SettingUI);
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
    void ReinforceBtn(PointerEventData evt)
    {
        UIManager.LoadUI(Define.UI_Type.ReinforcementUI);
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }


}
