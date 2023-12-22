using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Base
{
    enum Texts 
    {
        Result
    }
    enum Buttons 
    { 
        Restart
    }



    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.Restart).gameObject, Restart);
        ShowResult();

    }
    public void ShowResult()
    {
        
    }

    void Restart(PointerEventData evt)
    {
        
    }


}
