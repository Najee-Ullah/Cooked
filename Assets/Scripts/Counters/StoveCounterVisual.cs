using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] GameObject[] visualElements;

    [SerializeField] StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStoveUse +=StoveCounter_OnSwitch;
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
