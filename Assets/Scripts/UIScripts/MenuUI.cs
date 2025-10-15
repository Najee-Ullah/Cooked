using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour, IShow
{
    [SerializeField] Button StartButton;
    [SerializeField] Button QuitButton;
    [SerializeField] Button OptionsButton;
    [SerializeField] OptionsMenuUI OptionsMenuUI;
    [SerializeField] GameObject Visual;
    [SerializeField] TextMeshProUGUI highScoreText;

    private const string HIGHESTDELIVERIESMADE = "HighestDeliveriesMade";

    private void Awake()
    {
        StartButton.onClick.AddListener(() => { Loader.LoadScene("Main"); });
        OptionsButton.onClick.AddListener(() => { OptionsMenuUI.Show(); Hide(); });
        QuitButton.onClick.AddListener(() => { Application.Quit(); });
        StartButton.Select();
        if(PlayerPrefs.HasKey(HIGHESTDELIVERIESMADE))
        {
            highScoreText.text = "Current HighScore : " + PlayerPrefs.GetInt(HIGHESTDELIVERIESMADE);
        }
        else
        {
            highScoreText.text = "Current HighScore : 0";
        }
    }
    public void Show()
    {
        Visual.SetActive(true);
    }
    public void Hide()
    {
        Visual.SetActive(false);
    }
}
