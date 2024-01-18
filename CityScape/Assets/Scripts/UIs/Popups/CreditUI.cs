using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditUI : UI_Base
{

    enum Texts
    {
    }
    enum Buttons
    {
        OffCreditButton,
    }



    // Start is called before the first frame update
    void Start()
    {
        //Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        BindEvent(Get<Button>((int)Buttons.OffCreditButton).gameObject, DeleteBtn);

    }

    void DeleteBtn(PointerEventData evt)
    {
        Destroy(gameObject);
    }


}
