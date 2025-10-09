using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject Visual;
    [SerializeField] Image circularFill;
    [SerializeField] TextMeshProUGUI deliveryText;
    [SerializeField] Button RestartButton;
    float totalGameplayTimer;

    private void Start()
    {
        Hide();
        totalGameplayTimer = KitchenGameManager.Instance.GetGamePlayTimer();
        KitchenGameManager.Instance.OnGameOver += Instance_OnGameOver1;
        RestartButton.onClick.AddListener(() => { Loader.ReloadScene(); });
    }

    private void Instance_OnGameOver1(object sender, System.EventArgs e)
    {
        deliveryText.text += DeliveryCounter.Instance.GetDeliveredAmount().ToString();
        Show();
    }

    private void Update()
    {
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            circularFill.fillAmount = KitchenGameManager.Instance.GetGamePlayTimer() / totalGameplayTimer;
        }
    }

    private void Instance_OnGameOver(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Hide()
    {
        Visual.SetActive(false);
    }
    private void Show()
    {
        Visual.SetActive(true);
    }
}
