using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Button StartButton;
    [SerializeField] Button QuitButton;
    [SerializeField] Button OptionsButton;
    [SerializeField] OptionsMenuUI OptionsMenuUI;
    [SerializeField] GameObject Visual;

    private void Awake()
    {
        StartButton.onClick.AddListener(() => { Loader.LoadScene("Main"); });
        OptionsButton.onClick.AddListener(() => { OptionsMenuUI.Show(); Hide(); });
        QuitButton.onClick.AddListener(() => { Application.Quit(); });
        StartButton.Select();

    }
    //private void Show()
    //{
    //    Visual.SetActive(true);
    //}
    private void Hide()
    {
        Visual.SetActive(false);
    }
}
