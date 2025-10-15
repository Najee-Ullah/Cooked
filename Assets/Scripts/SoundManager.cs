
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SFXRefsSO SFXRefsSO;
    [SerializeField] AudioSource BackgroundMusic;
    public static SoundManager Instance;

    private float volumeMultiplier = .1f;
    private const string VOLUME = "volume";
    private bool IsBackgroundMusicEnabled = false;

    public enum CurrentScene
    {
        MainMenu,
        Game
    }
    [SerializeField] CurrentScene currentScene;
    private void Awake()
    {
        if(PlayerPrefs.HasKey(VOLUME)) 
          volumeMultiplier = PlayerPrefs.GetFloat(VOLUME);

        ToggleMusic();
        
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
        if (currentScene == CurrentScene.Game)
        {
            DeliveryCounter.Instance.OnOrderSuccess += DeliveryCounter_OrderSuccess;
            DeliveryCounter.Instance.OnOrderFail += DeliveryCounter_OrderFail;
            CuttingCounter.OnCutAll += CuttingCounter_OnCutAll;
            TrashCounter.onDump += TrashCounter_onDump;
            BaseCounter.onDropObject += BaseCounter_onDropObject;
            BaseCounter.onPickObject += BaseCounter_onPickObject;
            StoveCounterVisual.OnCautionWarning += StoveCounterVisual_OnCautionWarning;
            KitchenGameManager.Instance.OnCountChanged += Instance_OnCountChanged;
            PlateCounter.OnPlatePickUp += PlateCounter_OnPlateRemove;
            ClearCounter.OnPlaceInPlate += ClearCounter_OnPlaceInPlate;
            ContainerCounter.OnCreateObject += ContainerCounter_OnCreateObject;
        }
    }

    private void ContainerCounter_OnCreateObject(object sender, System.EventArgs e)
    {
        ContainerCounter containerCounter = sender as ContainerCounter;
        PlaySoundInArray(SFXRefsSO.objectPickUpSound, containerCounter.transform.position);

    }

    private void ClearCounter_OnPlaceInPlate(object sender, System.EventArgs e)
    {
        ClearCounter clearCounter = sender as ClearCounter;
        PlaySoundInArray(SFXRefsSO.objectDropSound, clearCounter.transform.position);
    }

    private void PlateCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        PlateCounter plateCounter = sender as PlateCounter;
        PlaySound(SFXRefsSO.plateSound, plateCounter.transform.position,volumeMultiplier);
    }

    private void Instance_OnCountChanged(object sender, KitchenGameManager.OnCountChangedEventArgs e)
    {
        if (e.count > 0)
            PlaySound(SFXRefsSO.warning[1], Camera.main.transform.position,volumeMultiplier);
        else
            PlaySound(SFXRefsSO.warning[0], Camera.main.transform.position,volumeMultiplier);
    }

    private void StoveCounterVisual_OnCautionWarning(object sender, System.EventArgs e)
    {
        StoveCounterVisual stoveCounterVisual = sender as StoveCounterVisual;
        PlaySoundInArray(SFXRefsSO.warning, stoveCounterVisual.transform.position);
    }

    private void BaseCounter_onPickObject(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySoundInArray(SFXRefsSO.objectPickUpSound, baseCounter.transform.position);
    }

    private void BaseCounter_onDropObject(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySoundInArray(SFXRefsSO.objectDropSound, baseCounter.transform.position);
    }

    private void TrashCounter_onDump(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySoundInArray(SFXRefsSO.trashSound, trashCounter.transform.position);
    }

    private void CuttingCounter_OnCutAll(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySoundInArray(SFXRefsSO.chopSound, cuttingCounter.transform.position);
    }

    private void DeliveryCounter_OrderFail(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = sender as DeliveryCounter;
        PlaySoundInArray(SFXRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryCounter_OrderSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = sender as DeliveryCounter;
        PlaySoundInArray(SFXRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySoundInArray(AudioClip[] clip, Vector3 position, float volume = 1f)
    {
        PlaySound(clip[Random.Range(0, clip.Length)], position);
    }
    private void PlaySound(AudioClip clip,Vector3 position,float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(clip, position, volume * volumeMultiplier);
    }
    public void AdjustVolume()
    {
        if(volumeMultiplier >= 1f)
        {
            volumeMultiplier = 0f;
        }
        else
        {
            volumeMultiplier += .1f;
        }
        PlayerPrefs.SetFloat(VOLUME, volumeMultiplier);
        PlayerPrefs.Save();
    }
    public int GetVolume()
    {
        return (int)(volumeMultiplier * 10);
    }
    public void ToggleMusic()
    {
        if (BackgroundMusic != null)
        {
            if (!IsBackgroundMusicEnabled)
            {
                IsBackgroundMusicEnabled = true;
                BackgroundMusic.volume = 1 * volumeMultiplier;
            }
            else
            {
                IsBackgroundMusicEnabled = false;
                BackgroundMusic.volume = 0 * volumeMultiplier;

            }
        }
    }
    public bool IsMusicEnabled()
    {
        return IsBackgroundMusicEnabled;
    }
}
