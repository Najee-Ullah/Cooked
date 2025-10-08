using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class InputSystem : MonoBehaviour
{
    Vector2 movementVector = Vector2.zero;

    public event EventHandler OnInteractAction;
    public event EventHandler OnAlternateInteractAction;
    public event EventHandler OnPauseAction;

    public static InputSystem Instance;
    public event Action OnRebindStart;
    public event Action OnRebindComplete;

    private InputActions PlayerInputActions;

    private const string SAVEDBINDINGS = "SavedBindings";

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        AlternateInteract,
        Pause
    }



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        PlayerInputActions = new InputActions();
        LoadBindings();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += Interact_performed;
        PlayerInputActions.Player.AlternateInteract.performed += AlternateInteract_performed;
        PlayerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void AlternateInteract_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAlternateInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    public Vector3 GetMovementInputNormalized()
    {
        movementVector = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 newPosition = new Vector3(movementVector.x, 0, movementVector.y);
        return newPosition.normalized;
    }
    
    public void StartRebind(Binding binding,Action<string> OnComplete)
    {
        OnRebindStart?.Invoke();
        string actionName;
        int bindingIndex;
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                actionName = "Move";
                bindingIndex = 1; 
                break;

            case Binding.Move_Down:
                actionName = "Move";
                bindingIndex = 2;
                break;

            case Binding.Move_Left:
                actionName = "Move";
                bindingIndex = 3; 
                break;

            case Binding.Move_Right:
                actionName = "Move";
                bindingIndex = 4; 
                break;

            case Binding.Interact:
                actionName = "Interact";
                bindingIndex = 0;
                break;

            case Binding.AlternateInteract:
                actionName = "AlternateInteract";
                bindingIndex = 0;
                break;

            case Binding.Pause:
                actionName = "Pause";
                bindingIndex = 0;
                break;
        }

        InputAction action = PlayerInputActions.FindAction(actionName);
        if(action != null)
        {
            action.Disable();
            action.PerformInteractiveRebinding(bindingIndex).OnComplete(operation =>
            {
                action.Enable();
                operation.Dispose();
                SaveBindings();
                OnComplete?.Invoke(action.GetBindingDisplayString(bindingIndex));
                OnRebindComplete.Invoke();
            }).Start();
            SaveBindings();
        }
    }

    public void LoadBindings()
    {
        PlayerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(SAVEDBINDINGS));
    }

    public void SaveBindings()
    {
        string savedBindings = PlayerInputActions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(SAVEDBINDINGS, savedBindings);
        PlayerPrefs.Save();

    }
    public void ResetBindings()
    {
        PlayerPrefs.DeleteKey(SAVEDBINDINGS);
    }
    public InputActions GetPlayerInputActions()
    {
        return PlayerInputActions;
    }
}
