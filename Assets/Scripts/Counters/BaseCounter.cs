using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    public static event EventHandler onDropObject;
    public static event EventHandler onPickObject;

    [SerializeField] private Transform kitchenObjectLocation;
    private KitchenObject kitchenObject = null;

    public static void ResetStaticData()
    {
        onDropObject = null;
        onPickObject = null;
    }

    public virtual void Interact(Player player) {
        Debug.LogError("Base Counter : Interact");
    }
    public virtual void AlternateInteract(Player player)
    {
        
    }
    public Transform GetKitchenObjectTransform()
    {
        return kitchenObjectLocation;
    }
    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
        onPickObject?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        onDropObject?.Invoke(this, EventArgs.Empty);
    }
    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }
}