using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler<EventArgs> onPlayerInteraction;

    public static event EventHandler OnCreateObject;

    public new static void ResetStaticData()
    {
        OnCreateObject = null;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.CreateKitchenObject(kitchenObjectSO, player);
            onPlayerInteraction?.Invoke(this, EventArgs.Empty);
            OnCreateObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
