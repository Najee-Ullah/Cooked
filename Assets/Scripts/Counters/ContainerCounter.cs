using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler<EventArgs> onPlayerInteraction;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.CreateKitchenObject(kitchenObjectSO, player);
            onPlayerInteraction?.Invoke(this, EventArgs.Empty);
        }
    }
}
