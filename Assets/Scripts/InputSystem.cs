using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputSystem : MonoBehaviour
{
    Vector2 movementVector = Vector2.zero;

    public event EventHandler OnInteractAction;
    public event EventHandler OnAlternateInteractAction;


    private InputActions PlayerInputActions;

    private void Awake()
    {
        PlayerInputActions = new InputActions();
        PlayerInputActions.Player.Enable();
        PlayerInputActions.Player.Interact.performed += Interact_performed;
        PlayerInputActions.Player.AlternateInteract.performed += AlternateInteract_performed;
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
}
