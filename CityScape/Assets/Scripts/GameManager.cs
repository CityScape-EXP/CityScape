using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject audioPanel;
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    public void ShowAudioPanel()
    {
        audioPanel.SetActive(true);
    }
}
