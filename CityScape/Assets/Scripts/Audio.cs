using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderBgm;
    [SerializeField] private Slider sliderSFX;
    public static Audio _instance = null;
    public static Audio instance { get { return _instance; }}

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        sliderBgm.onValueChanged.AddListener(SetBgmVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);

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

    public void SetBgmVolume(float volume)
    {
        audioMixer.SetFloat("bgm", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("bgm", volume);
        Debug.Log(PlayerPrefs.GetFloat("bgm"));
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfx", volume);
    }
}
