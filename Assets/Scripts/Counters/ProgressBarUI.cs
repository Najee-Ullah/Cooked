
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject targetObject;
    private IHasProgress progressObject;

    private void Awake()
    {
        progressObject = targetObject.GetComponent<IHasProgress>();
    }
    private void Start()
    {
        if (progressObject != null)
        {
            progressObject.OnProgressMade += ProgressObject_OnProgressMade;
            Hide();
        }
    }

    private void ProgressObject_OnProgressMade(object sender, IHasProgress.OnProgressEventArgs e)
    {
        progressBar.fillAmount = e.currentProgress;
        if (progressBar.fillAmount >= 1 || progressBar.fillAmount < 0.1)
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
