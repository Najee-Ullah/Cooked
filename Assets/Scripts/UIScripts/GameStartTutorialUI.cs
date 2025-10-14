using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameStartTutorialUI : MonoBehaviour
{
    [SerializeField] GameObject Visual;
    [Header("Key Text Elements")]
    [SerializeField] TextMeshProUGUI Up;
    [SerializeField] TextMeshProUGUI Down;
    [SerializeField] TextMeshProUGUI Left;
    [SerializeField] TextMeshProUGUI Right;
    [SerializeField] TextMeshProUGUI Interact;
    [SerializeField] TextMeshProUGUI Alt_Interact;
    [SerializeField] TextMeshProUGUI Pause;

    private void Start()
    {
        Show();
        KitchenGameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        SetBindingsText();
    }

    private void Instance_OnGameStateChanged(object sender, KitchenGameManager.OnGameSateChangedEventArgs e)
    {
        if(e.currentState != KitchenGameManager.State.GameToStart)
        {
            Hide();
        }
    }
    private void Hide()
    {
        Visual.SetActive(false);
    }
    private void Show()
    {
        Visual.SetActive(true);
    }

    private void SetBindingsText()
    {
        if(InputSystem.Instance != null)
        {
            var player = InputSystem.Instance.GetPlayerInputActions().Player;

            if (player.Move.bindings[0].isComposite)
            {
                Up.text = player.Move.GetBindingDisplayString(1);
                Down.text = player.Move.GetBindingDisplayString(2);
                Left.text = player.Move.GetBindingDisplayString(3);
                Right.text = player.Move.GetBindingDisplayString(4);
            }

            Interact.text = player.Interact.GetBindingDisplayString();
            Alt_Interact.text = player.AlternateInteract.GetBindingDisplayString();
            Pause.text = player.Pause.GetBindingDisplayString();

        }
        else
        {
            Debug.LogError("InputSystem Null Exception");
        }
    }
}
