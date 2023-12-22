using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingUI : UI_Base
{
    enum Texts
    {
    }
    enum Buttons
    {
        RestartButton,
        ContinueButton,
        GoToMenuButton,
        CreditButton,
        ExitButton,
        OffSettingButton,

    }
    enum Sliders
    {
        BgmSlider,
        SFXSlider,
    }




    // Start is called before the first frame update
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        BindButtonEvt();
        BindSliderEvt();
    }

    #region Button_Event

    void BindButtonEvt()
    {
        BindEvent(Get<Button>((int)Buttons.RestartButton).gameObject, RestartButton);
        BindEvent(Get<Button>((int)Buttons.ContinueButton).gameObject, ContinueButton);
        BindEvent(Get<Button>((int)Buttons.GoToMenuButton).gameObject, GoToMenuButton);
        BindEvent(Get<Button>((int)Buttons.CreditButton).gameObject, CreditButton);
        BindEvent(Get<Button>((int)Buttons.ExitButton).gameObject, ExitButton);
        BindEvent(Get<Button>((int)Buttons.OffSettingButton).gameObject, OffSettingButton);
    }

    void RestartButton(PointerEventData evt)    
    {
        UIManager.instance.OnRestartButton();
    }
    void ContinueButton (PointerEventData evt) 
    {
        UIManager.instance.OffPausePanel();
    }
    void GoToMenuButton (PointerEventData evt) 
    {
        UIManager.instance.GoMenuButton();
    }
    void CreditButton (PointerEventData evt) 
    {
        UIManager.LoadUI(Define.UI_Type.CreditUI);
    }
    void ExitButton (PointerEventData evt) 
    {
        UIManager.instance.OnExitButton();
    }
    void OffSettingButton(PointerEventData evt) 
    {
        Destroy(this.gameObject);
    }


    #endregion Button_Event

    #region Slider_Event

    void BindSliderEvt()
    {
        Get<Slider>((int)Sliders.BgmSlider).onValueChanged.AddListener(delegate { VolumeChange(Define.Sounds.BGM); });
        Get<Slider>((int)Sliders.SFXSlider).onValueChanged.AddListener(delegate { VolumeChange(Define.Sounds.SFX); });
    }
    void VolumeChange(Define.Sounds Sound)
    {
        float volume;
        if (Sound == Define.Sounds.BGM)
        {
            volume = Get<Slider>((int)Sliders.BgmSlider).value;
            GameManager.Sound.BGMVolume = volume;

        }
        else
        {
            volume = Get<Slider>((int)Sliders.SFXSlider).value;
            GameManager.Sound.SFXVolume = volume;

        }

        GameManager.Sound.SetVolume(Sound, volume);

    }
    #endregion Slider_Event

   
}
