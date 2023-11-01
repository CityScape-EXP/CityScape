using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderBgm;
    [SerializeField] private Slider sliderSFX;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("master"))
        {
            sliderMaster.value = PlayerPrefs.GetFloat("master");
        }
        else
        {
            sliderMaster.value = 0.75f;
        }

        if (PlayerPrefs.HasKey("bgm"))
        {
            sliderBgm.value = PlayerPrefs.GetFloat("bgm");
        }
        else
        {
            sliderBgm.value = 0.75f;
        }

        if (PlayerPrefs.HasKey("sfx"))
        {
            sliderSFX.value = PlayerPrefs.GetFloat("sfx");
        }
        else
        {
            sliderSFX.value = 0.75f;
        }
    }
    private void Start()
    { 
        sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        sliderBgm.onValueChanged.AddListener(SetBgmVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("master", volume);
    }
    public void SetBgmVolume(float volume)
    {
        audioMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgm", volume);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume);
    }
}
