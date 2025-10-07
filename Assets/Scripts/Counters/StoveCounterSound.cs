using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] AudioSource source;


    private void Start()
    {
        stoveCounter.OnStoveUse += StoveCounter_OnStoveUse;
    }

    private void StoveCounter_OnStoveUse(object sender, StoveCounter.OnStoveUseEventArgs e)
    {
        bool playSound;
        playSound = (e.currentState == StoveCounter.State.Fried || e.currentState == StoveCounter.State.Frying);
        if (playSound)
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }
    }
}
