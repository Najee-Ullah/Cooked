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
    private Animator anim;
    private const string BONUSTRIGGER = "BonusTrigger";
    float totalGameplayTimer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Hide();
        totalGameplayTimer = KitchenGameManager.Instance.GetGamePlayTimer();
        KitchenGameManager.Instance.OnGameOver += Instance_OnGameOver1;
        DeliveryCounter.Instance.OnOrderSuccess += Instance_OnOrderSuccess;
        RestartButton.onClick.AddListener(() => { Loader.ReloadScene(); });
    }

    private void Instance_OnOrderSuccess(object sender, System.EventArgs e)
    {
        anim.SetTrigger(BONUSTRIGGER);
    }

    private void Instance_OnGameOver1(object sender, System.EventArgs e)
    {
        deliveryText.text += DeliveryCounter.Instance.GetDeliveredAmount().ToString();
        Show();
        RestartButton.Select();
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
