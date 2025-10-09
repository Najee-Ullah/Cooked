using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    private Animator animator;

    private const string numberChangeTrigger  = "NumberChange";

    bool countComplete;

    private void Start()
    {
        countComplete = false;
        animator = GetComponent<Animator>();
        KitchenGameManager.Instance.OnCountChanged += Instance_OnCountChanged;
    }

    private void Instance_OnCountChanged(object sender, KitchenGameManager.OnCountChangedEventArgs e)
    {
        if(countComplete)
        {
            gameObject.SetActive(false);
        }
        if(e.count <=1)
        {
            countComplete = true;
        }
        if (countDownText != null)
        {
            animator.SetTrigger(numberChangeTrigger);
            countDownText.text = e.count.ToString();
        }
    }

}
