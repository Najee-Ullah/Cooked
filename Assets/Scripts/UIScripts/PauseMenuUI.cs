using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour,IShow
{
    [SerializeField] Button ResumeButton;
    [SerializeField] Button OptionsButton;
    [SerializeField] Button QuitButton;
    [SerializeField] GameObject Visual;
    [SerializeField] OptionsMenuUI OptionsMenuUI;

    [SerializeField] string MAINMENU = "MainMenu";

    private void Awake()
    {
        Hide();
        ResumeButton.onClick.AddListener(() => { KitchenGameManager.Instance.TogglePause(); });
        OptionsButton.onClick.AddListener(() => { OptionsMenuUI.Show();Hide(); });
        QuitButton.onClick.AddListener(() => { QuitToMainMenu(); });
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameStateChanged += KitchenGameManager_OnGameStateChanged;
    }


    private void KitchenGameManager_OnGameStateChanged(object sender, KitchenGameManager.OnGameSateChangedEventArgs e)
    {
        if(e.currentState == KitchenGameManager.State.GamePaused)
        {
            Show();
        }
        else
        {
            Hide();
            OptionsMenuUI.Hide();
        }
    }
    private void QuitToMainMenu()
    {
        KitchenGameManager.Instance.OnGameStateChanged -= KitchenGameManager_OnGameStateChanged;
        KitchenGameManager.Instance.TogglePause();
        Loader.LoadScene(MAINMENU);
    }
    public void Show()
    {
        Visual.SetActive(true);
        ResumeButton.Select();
    }
    public void Hide()
    {
        Visual.SetActive(false);
    }
}
