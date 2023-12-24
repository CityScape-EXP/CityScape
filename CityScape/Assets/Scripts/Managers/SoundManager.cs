using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SoundManager을 사용해 봅시다.
/// 1. BGM,SFX(효과음)을 Resources/Sounds/ 안에 넣는다.
/// 2. 노래의 이름을 Define에 넣는다. 이때 BGM으로 사용할 노래면 BGM에 SFX로 사용할 노래면 SFX에 넣는다.
///     (띄어쓰기,시작 숫자 혀용X, 한글도 가능한 넣지말자 (서비스 타겟 기종이 UTF_8 인코더를 지원하지 않을 수 있다. 무슨 소린지 모르겠으면 빼자) . MaxCount 가 없는건 문제없으나 MaxCount가 있는데 그 아래 어떤 enum이 있는것은 혀용하지 않는다.)
/// 3. GameManager.Sound.Play(Define.BGM.틀고싶은노래) 또는 (Define.SFX.틀고싶은효과음); 식으로 사용하자 
/// 
/// 효과음은 캐싱하고 BGM은 안한다. 메모리 부족할 것 같으면 Scene 전환할 때 Clear()한번씩 해주자
/// 근데 굳?이 할 필요 없을듯
/// </summary>
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sounds.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    float _bgmvolume = 1.0f;
    float _sfxvolume = 1.0f;

    public float BGMVolume { get { return _bgmvolume; } set { _bgmvolume = value; PlayerPrefs.SetFloat("BGMVolume", value >= 1 ? 1 : value); } }
    public float SFXVolume { get { return _sfxvolume; } set { _sfxvolume = value;  PlayerPrefs.SetFloat("SFXVolume", value >= 1 ? 1 : value); } }

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            UnityEngine.Object.DontDestroyOnLoad(root);

            for (Define.Sounds s = Define.Sounds.BGM; s < Define.Sounds.MaxCount; s++)
            {
                GameObject go = new GameObject { name = $"{s}" };
                _audioSources[(int)s] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 0.75f);
            _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
            _audioSources[(int)Define.Sounds.BGM].loop = true;
        }
        else
        {
            for (Define.Sounds s = Define.Sounds.BGM; s < Define.Sounds.MaxCount; s++)
            {
                GameObject go = root.transform.Find($"{s}").gameObject;
                _audioSources[(int)s] = go.GetComponent<AudioSource>();
            }

            _audioSources[(int)Define.Sounds.BGM].loop = true;
        }

        _bgmvolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        _sfxvolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetVolume(Define.Sounds.BGM, _bgmvolume);
        SetVolume(Define.Sounds.SFX, _sfxvolume);

    }

    /// <summary>
    /// SFX용 PlayOneShot으로 구현 
    /// </summary>
    /// <param name="SFXSound"> Define.SFX Enum 에서 가져오기를 바람 </param>
    /// <param name="volume"></param>

    public void Play(Define.SFX SFXSound, float volume = 1.0f)
    {
        string path = $"{SFXSound}";
        AudioClip audioClip = GetOrAddAudioClip(path, Define.Sounds.SFX);
        Play(audioClip, Define.Sounds.SFX, volume);
    }
    /// <summary>
    /// BGM용 Play로 구현
    /// </summary>
    /// <param name="BGMSound">Define.BGM Enum 에서 가져오기를 바람 </param>
    /// <param name="volume"></param>
    public void Play(Define.BGM BGMSound, float volume = 1.0f)
    {
        string path = $"{BGMSound}";
        AudioClip audioClip = GetOrAddAudioClip(path, Define.Sounds.BGM);
        Play(audioClip, Define.Sounds.BGM, volume);
    }

    void Play(AudioClip audioClip, Define.Sounds type = Define.Sounds.SFX, float volume = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sounds.BGM)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sounds.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sounds.SFX];
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sounds type = Define.Sounds.SFX)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sounds.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }

    public void SetVolume(Define.Sounds type, float volume )
    {
        _audioSources[(int)type].volume = volume;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }
}
