using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameUI : UI_Base
{
    enum Texts
    {
    }
    enum Buttons
    {
        Jump,
        PauseButton,
    }



    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.Jump).gameObject, Jump);
        BindEvent(Get<Button>((int)Buttons.PauseButton).gameObject, PauseButton);


    }

    void Jump(PointerEventData evt)
    {
        Player.instance.JumpUp();
    }
    void PauseButton(PointerEventData evt)
    {
        UIManager.LoadUI(Define.UI_Type.PauseUI);
        Time.timeScale = 0f;
        UIManager.pauseOnclicked = true;
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
}
