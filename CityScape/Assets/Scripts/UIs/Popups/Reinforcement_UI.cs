using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reinforcement_UI : UI_Base
{
    enum Texts 
    {
    }

    enum Buttons 
    {
        OffReinforceButton
    }

    private void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.OffReinforceButton).gameObject, Destroy);
    }

    private void Destroy(PointerEventData evt)
    {
        Destroy(gameObject);
    }
}
