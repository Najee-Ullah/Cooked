using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public static event EventHandler OnPlaceInPlate;

    public new static void ResetStaticData()
    {
        OnPlaceInPlate = null;
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            
        }
        else
        {
            if(player.HasKitchenObject())
            {
                if(player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    if (GetKitchenObject().tryGetPlate(out plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().getKitchenObjectSO()))
                        {
                            OnPlaceInPlate?.Invoke(this,EventArgs.Empty);
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
