using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingUI : UI_Base , IPointerClickHandler
{
    enum Texts
    {
    }
    enum Buttons
    {
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
        Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        
        BindButtonEvt();
        BindSliderEvt();

        Get<Slider>((int)Sliders.BgmSlider).value = GameManager.Sound.BGMVolume;
        Get<Slider>((int)Sliders.SFXSlider).value = GameManager.Sound.SFXVolume;
    }

    #region Button_Event

    void BindButtonEvt()
    {
        BindEvent(Get<Button>((int)Buttons.CreditButton).gameObject, CreditButton);
        BindEvent(Get<Button>((int)Buttons.ExitButton).gameObject, ExitButton);
        BindEvent(Get<Button>((int)Buttons.OffSettingButton).gameObject, OffSettingButton);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Sound.Play(Define.SFX.UI_touch_1);
    }
    void CreditButton (PointerEventData evt) 
    {
        UIManager.LoadUI(Define.UI_Type.CreditUI);
        GameManager.Sound.Play(Define.SFX.UI_select_1);
    }
    void ExitButton (PointerEventData evt) 
    {
        UIManager.instance.OnExitButton();
    }
    void OffSettingButton(PointerEventData evt) 
    {
        Destroy(this.gameObject);
        GameManager.Sound.Play(Define.SFX.UI_select_1);
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
