using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Tutorial : UI_Base
{
    enum Texts
    {
        Tutorial,
        Tutorial_text,
    }
    enum Buttons
    {
        Delete
    }



    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.Delete).gameObject, DeleteBtn);

        

    }

    void DeleteBtn(PointerEventData evt)
    {
        
    }

}
