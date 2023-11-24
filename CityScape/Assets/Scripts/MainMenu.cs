using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;
    [SerializeField] public GameObject mainMenu;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
}
