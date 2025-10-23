using JetBrains.Annotations;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 2f;
    [SerializeField] Image recipeImage;
    [SerializeField] private Animator visualAnimator;
    private Vector3 targetPos = Vector3.zero;
    private bool shouldDestroy = false;
    //RecipeSO desiredRecipe = null;

    private const string ISWALKING = "IsWalking";

    public event Action<Customer> OnCustomerDisable;

    public void SetupCustomer(RecipeSO recipe,float moveSpeed)
    {
        //desiredRecipe = recipe;
        recipeImage.sprite = recipe.RecipeIcon;
        movementSpeed = moveSpeed;
    }
    private void Update()
    {
        //keep moving until reached destination
        if (!HasReachedDestination())
        {
            visualAnimator.SetBool(ISWALKING, true);
            Vector3 moveDir = targetPos - transform.position;
            moveDir.Normalize();
            transform.position = Vector3.MoveTowards(transform.position,targetPos,movementSpeed * Time.deltaTime);

            if (moveDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime *movementSpeed);
            }
        }
        else if (shouldDestroy)
        {
            OnCustomerDisable?.Invoke(this);
            shouldDestroy = false;
        }
        if(HasReachedDestination())
        {
            visualAnimator.SetBool(ISWALKING, false);
        }
    }

    public void ReachTarget(Vector3 target)
    {
        this.targetPos = target;
    }

    public void DestroySelf(Transform target)
    {
        shouldDestroy = true;
        this.targetPos = target.position;
        if (HasReachedDestination())
            OnCustomerDisable?.Invoke(this);
    }

    private bool HasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPos) <= 0.05f;
    }
}