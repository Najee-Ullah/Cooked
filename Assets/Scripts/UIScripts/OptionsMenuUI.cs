using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    [Tooltip("Current Scenes Option Menu UI")] [SerializeField] GameObject MenuUI;
    [SerializeField] Button DefaultButton;
    [SerializeField] Button VolumeButton;
    [SerializeField] TextMeshProUGUI VolumeText;
    [SerializeField] Button BackToMenuButton;
    [SerializeField] Button ToggleMusicButton;
    [SerializeField] TextMeshProUGUI ToggleMusicText;
    [SerializeField] GameObject Visual;
    [SerializeField] GameObject KeyBindingVisual;

    [Header("Rebinding Buttons")]
    [SerializeField] Button Up;
    [SerializeField] TextMeshProUGUI Uptxt;
    [SerializeField] Button Down;
    [SerializeField] TextMeshProUGUI Downtxt;
    [SerializeField] Button Left;
    [SerializeField] TextMeshProUGUI Lefttxt;
    [SerializeField] Button Right;
    [SerializeField] TextMeshProUGUI Righttxt;
    [SerializeField] Button Interact;
    [SerializeField] TextMeshProUGUI Interacttxt;
    [SerializeField] Button AltInteract;
    [SerializeField] TextMeshProUGUI AltInteracttxt;
    [SerializeField] Button Escape;
    [SerializeField] TextMeshProUGUI Esctxt;



    private void Awake()
    {
        Interact.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.Interact, (string displayText) => { UpdateKeyVisual(Interacttxt,displayText); }); });
        AltInteract.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.AlternateInteract, (string displayText) => { UpdateKeyVisual(AltInteracttxt,displayText); }); });
        Up.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.Move_Up, (string displayText) => { UpdateKeyVisual(Uptxt,displayText); }); });
        Down.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.Move_Down, (string displayText) => { UpdateKeyVisual(Downtxt,displayText); }); });
        Left.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.Move_Left, (string displayText) => { UpdateKeyVisual(Lefttxt,displayText); }); });
        Right.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.Move_Right, (string displayText) => { UpdateKeyVisual(Righttxt,displayText); }); });
        Escape.onClick.AddListener(() => { InputSystem.Instance.StartRebind(InputSystem.Binding.Pause, (string displayText) => { UpdateKeyVisual(Esctxt,displayText); }); });
        

        VolumeButton.onClick.AddListener(() => { SoundManager.Instance.AdjustVolume();UpdateVisual(); });
        ToggleMusicButton.onClick.AddListener(() => { SoundManager.Instance.ToggleMusic(); UpdateVisual(); });
        BackToMenuButton.onClick.AddListener(() => { GoBackToMenu(); });

    }
    private void Start()
    {
        SetKeyBindings();
        KeyBindingVisual.SetActive(false);
        Hide();
    }


    private void UpdateVisual()
    {
        VolumeText.text = "Volume : " + SoundManager.Instance.GetVolume().ToString();
        ToggleMusicText.text = "Music : " + SoundManager.Instance.IsMusicEnabled().ToString();
    }
    private void SetKeyBindings()
    {
        var input = InputSystem.Instance.GetPlayerInputActions();

        var interact = input.Player.Interact;
        Interacttxt.text = interact.GetBindingDisplayString();

        var altInteract = input.Player.AlternateInteract;
        AltInteracttxt.text = altInteract.GetBindingDisplayString();

        var pause = input.Player.Pause;
        Esctxt.text = pause.GetBindingDisplayString();

        var move = input.Player.Move;
            Uptxt.text = move.GetBindingDisplayString(1);
            Downtxt.text = move.GetBindingDisplayString(2);
            Lefttxt.text = move.GetBindingDisplayString(3);
            Righttxt.text = move.GetBindingDisplayString(4);
    }

    private void UpdateKeyVisual(TextMeshProUGUI text,string displayText)
    {
        text.text = displayText;
    }
    public void Show()
    {
        VolumeButton.Select();
        UpdateVisual();
        Visual.SetActive(true);
    }
    public void Hide()
    {
        Visual.SetActive(false);
    }

    public void GoBackToMenu()
    {
        Hide();
        if (MenuUI.TryGetComponent<IShow>(out IShow hidable))
            hidable.Show();
        DefaultButton.Select();

    }

    private void OnEnable()
    {
        if (InputSystem.Instance != null)
        {
            InputSystem.Instance.OnRebindStart += () => KeyBindingVisual.SetActive(true);
            InputSystem.Instance.OnRebindComplete += () => KeyBindingVisual.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (InputSystem.Instance != null)
        {
            InputSystem.Instance.OnRebindStart -= () => KeyBindingVisual.SetActive(true);
            InputSystem.Instance.OnRebindComplete -= () => KeyBindingVisual.SetActive(false);
        }
    }
}
