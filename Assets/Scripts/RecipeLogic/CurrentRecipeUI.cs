using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRecipeUI : MonoBehaviour
{
    [SerializeField] private Transform recipeUITemplate;

    private void Awake()
    {
        recipeUITemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OrderAdded += Instance_OrderAdded;
        DeliveryManager.Instance.OrderRemoved += Instance_OrderRemoved;
        UpdateVisual();
    }

    private void Instance_OrderRemoved(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void Instance_OrderAdded(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == recipeUITemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetCurrentOrderList())
        {
            Transform iconTransform = Instantiate(recipeUITemplate, transform);
            iconTransform.GetComponent<CurrentRecipeSingleUI>().SetKitchenObjectSO(recipeSO);
            iconTransform.gameObject.SetActive(true);
        }
    }

}
