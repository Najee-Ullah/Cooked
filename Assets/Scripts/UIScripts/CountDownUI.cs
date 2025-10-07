using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;

    private void Update()
    {
        if (countDownText != null)
        {
            countDownText.text = KitchenGameManager.Instance.GetCountDownTimer().ToString("#");
        }
    }
}
