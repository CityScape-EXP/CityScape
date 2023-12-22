using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameScene : UI_Base
{

    enum Buttons 
    { 
        Start,
    }
    enum Texts
    {
        StartTxt,
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        BindEvent(Get<Button>((int)Buttons.Start).gameObject, GameStart);

    }
    void GameStart(PointerEventData evt)
    {
        Destroy(this.gameObject);
    }



}
