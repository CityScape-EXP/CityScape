using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class TitleUI : UI_Base
{
    enum Texts
    {
        StartText,
        TitleText,
    }
    enum Buttons
    {

        GetMainMenu,

    }



    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        GameManager.Sound.Play(Define.BGM.main_BGM);
        BindEvent(Get<Button>((int)Buttons.GetMainMenu).gameObject, GetMainMenu);

    }

    

    void GetMainMenu(PointerEventData evt)
    {
        UIManager.LoadUI(Define.UI_Type.MainMenuUI);
    }
}