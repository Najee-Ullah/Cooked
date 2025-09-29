using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] PlateCounter platesCounter;
    [SerializeField] private Transform plateVisual;
    [SerializeField] Transform counterTopPoint;

    private List<GameObject> plateVisualList;

    private void Start()
    {
        plateVisualList = new List<GameObject>();
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlatesCounter_OnPlateRemove;
    }

    private void PlatesCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject toBeRemoved = (plateVisualList[plateVisualList.Count - 1].gameObject);
        plateVisualList.Remove(plateVisualList[plateVisualList.Count - 1]);
        Destroy(toBeRemoved);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual, counterTopPoint);
        float plateOffset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateVisualList.Count * plateOffset, 0);
        plateVisualList.Add(plateVisualTransform.gameObject);
    }
}
