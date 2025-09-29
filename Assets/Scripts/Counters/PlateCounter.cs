using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private float replaceTime;
    [SerializeField] private int maxPlates;
    [SerializeField] private KitchenObjectSO plate;
    private int currentNoOfPlates;
    private float currentTime;

    public event EventHandler OnPlateSpawn;

    public event EventHandler OnPlateRemove;

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > replaceTime)
        {
            currentTime = 0;
            if (currentNoOfPlates <= maxPlates)
            {
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
                currentNoOfPlates++;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (currentNoOfPlates > 0)
            {
                KitchenObject.CreateKitchenObject(plate, player);
                currentNoOfPlates --;
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
