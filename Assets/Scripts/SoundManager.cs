
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SFXRefsSO SFXRefsSO;

    private void Start()
    {
        DeliveryCounter.Instance.OrderSuccess += DeliveryCounter_OrderSuccess;
        DeliveryCounter.Instance.OrderFail += DeliveryCounter_OrderFail;
        CuttingCounter.OnCutAll += CuttingCounter_OnCutAll;
        TrashCounter.onDump += TrashCounter_onDump;
        BaseCounter.onDropObject += BaseCounter_onDropObject;
        BaseCounter.onPickObject += BaseCounter_onPickObject;
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
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
