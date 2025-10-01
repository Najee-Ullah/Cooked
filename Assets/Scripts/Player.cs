using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour,IKitchenObjectParent
{
    //MovementParameters
    [SerializeField] private float movementSpeed = 7f,rotationSpeed = 4f;
    //ReferenceToInputSystem
    [SerializeField] private InputSystem inputSystem;
    //Conditionals
    bool isWalking;
    bool canMove;
    //CharacterDimensions
    float playerHeight = 2f,playerRadius = 0.7f;
    [SerializeField] Transform kitchenObjectLocation;
    //Interactions
    private KitchenObject kitchenObject;
    private float interactDistance = 2f;
    private Vector3 lastInteractionDir;
    public LayerMask interactable;
    BaseCounter selectedCounter;
    //--events
    public event EventHandler <OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    public static Player Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Error-More than one player instances");
        }
        Instance = this;
    }
    private void Start()
    {
        inputSystem.OnInteractAction += InputSystem_OnInteractAction;
        inputSystem.OnAlternateInteractAction += InputSystem_OnAlternateInteractAction;
    }

    private void InputSystem_OnAlternateInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.AlternateInteract(this);
        }
    }

    private void InputSystem_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {

        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            HandleMovement();

            HandleInteractions();

        }
    }

    private void HandleInteractions()
    {
        Vector3 moveDir = inputSystem.GetMovementInputNormalized();
        if (moveDir != Vector3.zero)
        {
           lastInteractionDir = inputSystem.GetMovementInputNormalized();
        }
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit hit, interactDistance,interactable))
        {
            if(hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (selectedCounter != baseCounter)
                {
                    selectedCounter = baseCounter;
                    ChangeSelectedCounter(selectedCounter);
                }

            }
            else
            {
                selectedCounter = null;
                ChangeSelectedCounter(selectedCounter);
            }
        }
        else
        {
            selectedCounter = null;
            ChangeSelectedCounter(selectedCounter);
        }
        
    }

    private void HandleMovement()
    {
        isWalking = false;

        Vector3 moveDir = inputSystem.GetMovementInputNormalized();

        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, movementSpeed * Time.deltaTime);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x !=0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, movementSpeed * Time.deltaTime);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, movementSpeed * Time.deltaTime);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            isWalking = moveDir != Vector3.zero;
            transform.position += moveDir * Time.deltaTime * movementSpeed;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
        }
    }

    public bool getWalking()
    {
        return isWalking;
    }
    void ChangeSelectedCounter(BaseCounter selectedCounter)
    {
            OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
            {
                selectedCounter = selectedCounter
            });
    }

    public Transform GetKitchenObjectTransform()
    {
        return kitchenObjectLocation;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
