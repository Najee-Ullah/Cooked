using System.Collections;
using UnityEngine;

public class DeliveryCounterUI : MonoBehaviour
{
    private Animator animator;
    private const string SUCCESS = "Success";
    private const string FAIL = "Fail";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryCounter.Instance.OnOrderSuccess += Instance_OnOrderSuccess;
        DeliveryCounter.Instance.OnOrderFail += Instance_OnOrderFail;
    }

    private void Instance_OnOrderFail(object sender, System.EventArgs e)
    {
        animator.SetTrigger(FAIL);
    }

    private void Instance_OnOrderSuccess(object sender, System.EventArgs e)
    {
        animator.SetTrigger(SUCCESS);
    }
}