using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject[] visualElements;

    [SerializeField] StoveCounter stoveCounter;

    [SerializeField] Image CautionVisual;

    private const string BURNING = "Burning";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        stoveCounter.OnStoveUse +=StoveCounter_OnSwitch;
        stoveCounter.OnBurning += StoveCounter_OnBurning;

        CautionVisual.gameObject.SetActive(false);
    }

    private void StoveCounter_OnBurning(object sender, StoveCounter.OnBurningEventArgs e)
    {
        float cautionVisualDisable = .2f;
        if(e.kitchenObjectBurnTimer - e.burnTimer <=e.kitchenObjectBurnTimer/2f && e.kitchenObjectBurnTimer - e.burnTimer >= cautionVisualDisable)
        {
            CautionVisual.gameObject.SetActive(true);
            animator.SetBool(BURNING, true);
        }
        else 
        {
            CautionVisual.gameObject.SetActive(false);
            animator.SetBool(BURNING, false);

        }
    }

    private void StoveCounter_OnSwitch(object sender, StoveCounter.OnStoveUseEventArgs e)
    {
        if(e.currentState == StoveCounter.State.Frying || e.currentState == StoveCounter.State.Fried)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach (GameObject element in visualElements)
        {
            element.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach(GameObject element in visualElements)
        {
            element.SetActive(false);
        }
    }

}
