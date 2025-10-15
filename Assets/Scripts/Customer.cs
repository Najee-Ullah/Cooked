using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] Image recipeImage;
    private Vector3 targetPos = Vector3.zero;
    private bool shouldDestroy = false;
    //RecipeSO desiredRecipe = null;

    public event Action<Customer> OnCustomerDisable;

    public void SetCustomerOrder(RecipeSO recipe)
    {
        //desiredRecipe = recipe;
        recipeImage.sprite = recipe.RecipeIcon;
        
    }
    private void Update()
    {
        //keep moving until reached destination
        if (!hasReachedDestination())
        {
            Vector3 moveDir = targetPos - transform.position;
            moveDir.Normalize();
            transform.position = Vector3.MoveTowards(transform.position,targetPos,movementSpeed * Time.deltaTime);


        }
        else if (shouldDestroy)
        {
            OnCustomerDisable?.Invoke(this);
            shouldDestroy = false;
        }
    }

    public void ReachTarget(Vector3 target)
    {
        this.targetPos = target;
        transform.LookAt(targetPos);
    }

    public void DestroySelf(Transform target)
    {
        shouldDestroy = true;
        this.targetPos = target.position;
        if (hasReachedDestination())
            OnCustomerDisable?.Invoke(this);
    }

    private bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPos) <= 0.05f;
    }
}