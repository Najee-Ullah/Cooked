using System.Collections;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{

    private void ResetStaticEvents()
    {
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
        CuttingCounter.ResetStaticData();
    }
    private void Start()
    {
        ResetStaticEvents();
    }
}