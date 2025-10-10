using System.Collections;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{

    private void ResetStaticEvents()
    {
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
        StoveCounterVisual.ResetStaticData();
        PlateCounter.ResetStaticData();
        ClearCounter.ResetStaticData();
        ContainerCounter.ResetStaticData();
    }
    private void Start()
    {
        ResetStaticEvents();
    }
}