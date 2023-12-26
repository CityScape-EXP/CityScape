using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClearUI : UI_Base
{
    enum Texts
    {
        Result
    }
    enum Buttons
    {
        RestartButton,
        GoToMenuButton,
    }

    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.RestartButton).gameObject, Restart);
        BindEvent(Get<Button>((int)Buttons.GoToMenuButton).gameObject, GoToMenuButton);
        ShowResult();
    }
    public void ShowResult()
    {

    }

    void Restart(PointerEventData evt)
    {
        UIManager.instance.OnRestartButton();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }

    void GoToMenuButton(PointerEventData evt)
    {
        UIManager.instance.GoMenuButton();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }


}
