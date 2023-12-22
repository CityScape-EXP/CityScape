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
    }



    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.Jump).gameObject, Jump);



    }

    void Jump(PointerEventData evt)
    {
        Player.instance.JumpUp();
    }
}
