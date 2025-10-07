using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCounter : BaseCounter
{
    public static event EventHandler onDump;

    public new static void ResetStaticData()
    {
        onDump = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            onDump?.Invoke(this,EventArgs.Empty);
        }
            
    }
}
