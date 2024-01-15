using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Reinforcement_UI : UI_Base , IPointerClickHandler
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
        Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        BindEvent(Get<Button>((int)Buttons.OffReinforceButton).gameObject, Destroy);
    }

    private void Destroy(PointerEventData evt)
    {
        Destroy(gameObject);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Sound.Play(Define.SFX.UI_touch_1);
    }
}
