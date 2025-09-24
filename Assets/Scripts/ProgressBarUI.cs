using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private CuttingCounter cuttingCounter;

    private void Start()
    {
        cuttingCounter.CutProgressMade += CuttingCounter_CutProgressMade;
        Hide();
    }

    private void CuttingCounter_CutProgressMade(object sender, CuttingCounter.OnCutProgressEventArgs e)
    {
        progressBar.fillAmount = e.cutProgress;
        if(progressBar.fillAmount >= 1 || progressBar.fillAmount <0.1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    private void Show()
    {
        this.gameObject.SetActive(true);
    }
    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
