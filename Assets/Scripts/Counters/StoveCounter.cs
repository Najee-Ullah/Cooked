using System;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{

    [SerializeField] FryingKitchenObjectSO[] fryingKitchenObjectSOs;
    [SerializeField] BurningKitchenObjectSO[] burningKitchenObjectSOs;


    public event EventHandler<OnStoveUseEventArgs> OnStoveUse;
    public class OnStoveUseEventArgs : EventArgs
    {
        public State currentState;

    }
    public event EventHandler<IHasProgress.OnProgressEventArgs> OnProgressMade;

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }
    float fryingTimer = 0f, burnTimer = 0f;

    private State state = State.Idle;

    FryingKitchenObjectSO fryingKitchenObjectSO;
    BurningKitchenObjectSO burningKitchenObjectSO;

    public event EventHandler<OnBurningEventArgs> OnBurning;
    public class OnBurningEventArgs {
        public float burnTimer;
        public float kitchenObjectBurnTimer;
    }


    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if(GetKitchenObject() != null)
                {
                    fryingKitchenObjectSO = GetFryingTargetKitchenObjectSO(GetKitchenObject().getKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0f;
                    OnStoveUse?.Invoke(this, new OnStoveUseEventArgs { currentState = state });
                }
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                OnProgressMade?.Invoke(this, new IHasProgress.OnProgressEventArgs { currentProgress = (float)fryingTimer / fryingKitchenObjectSO.fryingTimer});
                if (fryingTimer > fryingKitchenObjectSO.fryingTimer)
                {
                    GetKitchenObject().DestroySelf();
                    ClearKitchenObject();
                    KitchenObject.CreateKitchenObject(fryingKitchenObjectSO.Output, this);
                    burnTimer = 0;
                    state = State.Fried;
                    OnStoveUse?.Invoke(this, new OnStoveUseEventArgs { currentState = state });
                    burningKitchenObjectSO = GetBurningTargetKitchenObjectSO(fryingKitchenObjectSO.Output);

                }
                break;
            case State.Fried:
                burnTimer += Time.deltaTime;
                OnProgressMade?.Invoke(this, new IHasProgress.OnProgressEventArgs { currentProgress = (float)burnTimer / burningKitchenObjectSO.burningTimer});
                OnBurning?.Invoke(this, new OnBurningEventArgs { kitchenObjectBurnTimer = burningKitchenObjectSO.burningTimer,burnTimer = burnTimer });
                if (burnTimer > burningKitchenObjectSO.burningTimer)
                {
                    GetKitchenObject().DestroySelf();
                    ClearKitchenObject();
                    KitchenObject.CreateKitchenObject(burningKitchenObjectSO.Output, this);
                    state = State.Burned;
                    OnStoveUse?.Invoke(this, new OnStoveUseEventArgs { currentState = state });
                }
                break;
            case State.Burned:
                //do nothing
                break;
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (isValidCookingItem(player.GetKitchenObject().getKitchenObjectSO()))
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
                    {
                        GetKitchenObject().DestroySelf();
                        state = State.Idle;
                        OnStoveUse?.Invoke(this, new OnStoveUseEventArgs { currentState = state });
                        OnProgressMade?.Invoke(this, new IHasProgress.OnProgressEventArgs { currentProgress = 0f });
                    }
                }
            }
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStoveUse?.Invoke(this, new OnStoveUseEventArgs { currentState = state });
                OnProgressMade?.Invoke(this, new IHasProgress.OnProgressEventArgs { currentProgress = 0f });
            }
        }
    }
    private bool isValidCookingItem(KitchenObjectSO kitchenObject)
    {
        foreach (FryingKitchenObjectSO kSO in fryingKitchenObjectSOs)
        {
            if (kitchenObject == kSO.Input)
            {
                return true;
            }
        }
        return false;
    }

    private FryingKitchenObjectSO GetFryingTargetKitchenObjectSO(KitchenObjectSO kitchenObject)
    {
        foreach (FryingKitchenObjectSO kSO in fryingKitchenObjectSOs)
        {
            if (kitchenObject == kSO.Input)
            {
                return kSO;
            }
        }
        return null;
    }
    private BurningKitchenObjectSO GetBurningTargetKitchenObjectSO(KitchenObjectSO kitchenObject)
    {
        foreach (BurningKitchenObjectSO kSO in burningKitchenObjectSOs)
        {
            if (kitchenObject == kSO.Input)
            {
                return kSO;
            }
        }
        return null;
    }
}