using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    [SerializeField] Button VolumeButton;
    [SerializeField] TextMeshProUGUI VolumeText;
    [SerializeField] Button BackToMenuButton;
    [SerializeField] GameObject Visual;

    private void Awake()
    {
        VolumeButton.onClick.AddListener(() => { SoundManager.Instance.AdjustVolume();UpdateVisual(); });
        BackToMenuButton.onClick.AddListener(() => { Hide(); });
    }
    private void Start()
    {
        Hide();
    }
    private void UpdateVisual()
    {
        VolumeText.text = "Volume : " + SoundManager.Instance.GetVolume().ToString();
    }
    public void Show()
    {
        UpdateVisual();
        Visual.SetActive(true);
    }
    public void Hide()
    {
        Visual.SetActive(false);
    }
}
