using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRecipeSingleUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Transform iconTransform;
    [SerializeField] private TextMeshProUGUI recipeName;

    public void SetKitchenObjectSO(RecipeSO recipeSO)
    {
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOs)
        {
            icon.gameObject.SetActive(true);
            icon.sprite = kitchenObjectSO.sprite;
            Instantiate(icon, iconTransform);
            icon.gameObject.SetActive(false);
        }
        recipeName.text = recipeSO.name;
    }
}
