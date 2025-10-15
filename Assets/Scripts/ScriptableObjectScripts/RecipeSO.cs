using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject
{
    [SerializeField] public List<KitchenObjectSO> kitchenObjectSOs;
    [SerializeField] public string RecipeName;
    [SerializeField] public Sprite RecipeIcon; 
}
