using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;
    public static bool isStart = true;
    [SerializeField] public GameObject mainMenu;
    [SerializeField] public GameObject Title;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    private void Start()
    {
        if(isStart ==false)
        {
            mainMenu.SetActive(true);
            isStart = true;
        }
    }
}
