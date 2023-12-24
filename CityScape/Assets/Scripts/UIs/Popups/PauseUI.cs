using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseUI : UI_Base
{
    enum Texts
    {
    }
    enum Buttons
    {
        RestartButton,
        ContinueButton,
        GoToMenuButton,
    }
    enum Sliders
    {
        BgmSlider,
        SFXSlider,
    }
    void Start()
    {
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));


        BindButtonEvt();
        BindSliderEvt();

        Get<Slider>((int)Sliders.BgmSlider).value = GameManager.Sound.BGMVolume;
        Get<Slider>((int)Sliders.SFXSlider).value = GameManager.Sound.SFXVolume;
    }

    void BindButtonEvt()
    {
        BindEvent(Get<Button>((int)Buttons.RestartButton).gameObject, RestartButton);
        BindEvent(Get<Button>((int)Buttons.ContinueButton).gameObject, ContinueButton);
        BindEvent(Get<Button>((int)Buttons.GoToMenuButton).gameObject, GoToMenuButton);
    }

    void RestartButton(PointerEventData evt)
    {
        UIManager.instance.OnRestartButton();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
    void ContinueButton(PointerEventData evt)
    {
        UIManager.instance.OffPausePanel();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }
    void GoToMenuButton(PointerEventData evt)
    {
        UIManager.instance.GoMenuButton();
        GameManager.Sound.Play(Define.SFX.UI_select_1128);
    }

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
