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

    private void Awake()
    {
        StartButton.onClick.AddListener(() => { Loader.LoadScene("Main"); });
        QuitButton.onClick.AddListener(() => { Application.Quit(); });
        StartButton.Select();

    }
}
