using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    [SerializeField] CuttingKitchenObjectSO[] cuttingKitchenObjectSOs;

    public static event EventHandler OnCutAll;

    public event EventHandler OnCut;
    public event EventHandler<IHasProgress.OnProgressEventArgs> OnProgressMade;


    int cuttingProgress = 0;

    public new static void ResetStaticData()
    {
        OnCutAll = null;
    }
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
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().getKitchenObjectSO()))
                        GetKitchenObject().DestroySelf();
                }
            }
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
                OnProgressMade?.Invoke(this, new IHasProgress.OnProgressEventArgs { currentProgress = (float)cuttingProgress / target.numberOfCuts });
                if (cuttingProgress >= target.numberOfCuts)
                {

                    GetKitchenObject().DestroySelf();
                    ClearKitchenObject();
                    KitchenObject.CreateKitchenObject(target.Output, this);
                    cuttingProgress = 0;
                }
                    OnCut?.Invoke(this, EventArgs.Empty);
                    OnCutAll?.Invoke(this, EventArgs.Empty);
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
