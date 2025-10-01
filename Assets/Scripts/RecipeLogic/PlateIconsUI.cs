using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform IconTemplate;

    private void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnAddIngredient += PlateKitchenObject_OnAddIngredient;
    }

    private void PlateKitchenObject_OnAddIngredient(object sender, PlateKitchenObject.OnAddEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == IconTemplate) continue;    
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(IconTemplate, transform);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
            iconTransform.gameObject.SetActive(true);

        }
    }
}
