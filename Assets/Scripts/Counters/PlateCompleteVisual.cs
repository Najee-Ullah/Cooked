using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateObject[] plateObjectArray;
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [Serializable] private struct PlateObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject kitchnObjectVisual;
    }

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
        foreach (PlateObject obj in plateObjectArray)
        {
                obj.kitchnObjectVisual.SetActive(false);            
        }
    }

    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddEventArgs e)
    {
        foreach (PlateObject obj in plateObjectArray)
        {
            if(obj.kitchenObjectSO == e.kitchenObjectSO)
            {
                obj.kitchnObjectVisual.SetActive(true);
            }
        }
    }
}
