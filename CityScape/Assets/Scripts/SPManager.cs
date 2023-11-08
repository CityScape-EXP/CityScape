using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SPManager : MonoBehaviour
{
    [Header("settingCanvas")]
    [SerializeField] private GameObject settingCanvas;
    [Header("MainUI")]
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject offSettingPanelButton;
    [SerializeField] private GameObject creditPanelButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject settingText;

    [Header("GameUI")]
    [SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject goToMenuButton;
    private void OnEnable()
    {
        if (UIManager.isMenu == true && UIManager.isGame == false)
        {
            offSettingPanelButton.SetActive(true);
            creditPanelButton.SetActive(true);
            exitButton.SetActive(true);
            settingText.SetActive(true);

            pauseText.SetActive(false);
            continueButton.SetActive(false);
            restartButton.SetActive(false);
            goToMenuButton.SetActive(false);
        }
        else if (UIManager.isMenu == false && UIManager.isGame == true)
        {
            offSettingPanelButton.SetActive(false);
            creditPanelButton.SetActive(false);
            exitButton.SetActive(false);
            settingText.SetActive(false);

            pauseText.SetActive(true);
            continueButton.SetActive(true);
            restartButton.SetActive(true);
            goToMenuButton.SetActive(true);
        }
    }
}
