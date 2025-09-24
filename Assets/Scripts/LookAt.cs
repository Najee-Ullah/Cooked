using System.Collections;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public enum mode {
        LookTowards,
        LookAway,
        LookForward,
        LookBack
    }
    public mode Mode;

    private void Update()
    {
        switch (Mode)
        {
            case mode.LookTowards:
                Debug.Log("lookTowards");
                break;
            case mode.LookAway:
                Debug.Log("LookAway");
                break;
            case mode.LookForward:
                LookForward();
                break;
            default:
                Debug.Log("Default");
                break;
        }
    }
    private void LookForward()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
