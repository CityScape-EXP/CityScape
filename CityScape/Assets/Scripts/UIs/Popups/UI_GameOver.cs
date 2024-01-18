using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Base
{
    enum Texts 
    {
        Score,
        HighScore,
        GainCoin
    }
    enum Buttons 
    { 
        RestartButton,
        GoToMenuButton,
    }



    // Start is called before the first frame update
    void Start()
    {
        //Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.RestartButton).gameObject, Restart);
        BindEvent(Get<Button>((int)Buttons.GoToMenuButton).gameObject, GoToMenuButton);
        ShowResult();

    }
    public void ShowResult()
    {
        Get<TMP_Text>((int)Texts.Score).text = ScoreManager.instance.Score.ToString();
        Get<TMP_Text>((int)Texts.HighScore).text = DataManager.MainGameData.stageHighScore[(int)DataManager.NowStage].ToString();
        Get<TMP_Text>((int)Texts.GainCoin).text = GetMoney.getMoney.ToString();

    }

    void Restart(PointerEventData evt)
    {
        UIManager.instance.OnRestartButton();
        GameManager.Sound.Play(Define.SFX.UI_select_1);
    }

    void GoToMenuButton(PointerEventData evt)
    {
        UIManager.instance.GoMenuButton();
        GameManager.Sound.Play(Define.SFX.UI_select_1);
    }


}
