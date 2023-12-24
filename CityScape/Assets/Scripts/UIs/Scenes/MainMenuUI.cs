using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : UI_Base
{
    enum Texts
    {
        Stage1_Text, 
        Stage2_Text, 
        Stage3_Text,
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

        BindEvent(Get<Button>((int)Buttons.Stage1_Button).gameObject, Stage1Btn);
        BindEvent(Get<Button>((int)Buttons.Stage2_Button).gameObject, Stage2Btn);
        BindEvent(Get<Button>((int)Buttons.Stage3_Button).gameObject, Stage3Btn);
        BindEvent(Get<Button>((int)Buttons.SettingButton).gameObject, SettingBtn);
        BindEvent(Get<Button>((int)Buttons.ReinforceButton).gameObject, ReinforceBtn);

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
