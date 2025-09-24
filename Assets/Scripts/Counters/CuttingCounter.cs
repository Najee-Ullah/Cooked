using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO slicedTomato;
    [SerializeField] CuttingKitchenObjectSO[] cuttingKitchenObjectSOs;

    public event EventHandler OnCut;
    public event EventHandler<OnCutProgressEventArgs> CutProgressMade;
    public class OnCutProgressEventArgs : EventArgs
    {
        public float cutProgress;
    }

    int cuttingProgress = 0;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (isValidCuttingItem(player.GetKitchenObject().getKitchenObjectSO())) 
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }

        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                cuttingProgress = 0;
            }
        }
    }
    public override void AlternateInteract(Player player)
    {
        if (HasKitchenObject())
        {
            CuttingKitchenObjectSO target = GetTargetKitchenObjectSO(GetKitchenObject().getKitchenObjectSO());
            if (target != null)
            {
                cuttingProgress++;
                CutProgressMade?.Invoke(this, new OnCutProgressEventArgs { cutProgress = (float)cuttingProgress / target.numberOfCuts });
                if (cuttingProgress >= target.numberOfCuts)
                {

                    GetKitchenObject().DestroySelf();
                    ClearKitchenObject();
                    KitchenObject.CreateKitchenObject(target.Output, this);
                    cuttingProgress = 0;
                }
                else
                {
                    OnCut?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    private bool isValidCuttingItem(KitchenObjectSO kitchenObject)
    {
        foreach (CuttingKitchenObjectSO kSO in cuttingKitchenObjectSOs)
        {
            if (kitchenObject == kSO.Input)
            {
                return true;
            }
        }
        return false;
    }
    //private KitchenObjectSO GetOutputforKitchenObject(KitchenObjectSO kitchenObject)
    //{        
    //    CuttingKitchenObjectSO kSO = GetTargetKitchenObjectSO(kitchenObject);
    //    return kSO.Output;
    //}
    private CuttingKitchenObjectSO GetTargetKitchenObjectSO(KitchenObjectSO kitchenObject)
    {
        foreach (CuttingKitchenObjectSO kSO in cuttingKitchenObjectSOs)
        {
            if (kitchenObject == kSO.Input)
            {
                return kSO;
            }
        }
        return null;
    }
}
