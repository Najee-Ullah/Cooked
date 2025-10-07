using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
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
        OptionsButton.onClick.AddListener(() => { OptionsMenuUI.Show(); });
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
        }
    }
    private void QuitToMainMenu()
    {
        KitchenGameManager.Instance.OnGameStateChanged -= KitchenGameManager_OnGameStateChanged;
        KitchenGameManager.Instance.TogglePause();
        Loader.LoadScene(MAINMENU);
    }
    private void Show()
    {
        Visual.SetActive(true);
    }
    private void Hide()
    {
        Visual.SetActive(false);
    }
}
