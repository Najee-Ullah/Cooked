using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{

    [SerializeField] private Transform kitchenObjectLocation;
    private KitchenObject kitchenObject = null;

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
        return this.kitchenObject != null;
    }
}