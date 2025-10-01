using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private KitchenObjectSO[] validKitchenObjects;

    private List<KitchenObjectSO> kitchenObjectSOList;

    public event EventHandler <OnAddEventArgs> OnAddIngredient;
    public class OnAddEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Any(ko => ko.objectName == kitchenObjectSO.objectName))
        {
            return false;
        }
        if (validKitchenObjects.Any(ko => ko.objectName == kitchenObjectSO.objectName))
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnAddIngredient?.Invoke(this, new OnAddEventArgs { kitchenObjectSO = kitchenObjectSO });
            return true;
        }
        return false;
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
