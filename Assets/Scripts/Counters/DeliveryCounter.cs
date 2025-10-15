
using System;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {get;private set; }

    public event EventHandler OnOrderSuccess;
    public event EventHandler OnOrderFail;

    private int deliveriesCompleted = 0; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;


    }

    public override void Interact(Player player)
    {
        if (player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            List<KitchenObjectSO> kSOList = plateKitchenObject.GetKitchenObjectSOList();
            List<RecipeSO> currentOrderList = DeliveryManager.Instance.GetCurrentOrderList();
            bool found = false;
            if (kSOList.Count == currentOrderList[0].kitchenObjectSOs.Count)
            {
                bool correctOrder = true;
                for (int j = 0; j < kSOList.Count; j++)
                {
                    if (currentOrderList[0].kitchenObjectSOs.Contains(kSOList[j]))
                    {
                        //nothing
                    }
                    else
                    {
                        correctOrder = false;
                    }
                }
                if (correctOrder)
                {
                    plateKitchenObject.DestroySelf();
                    currentOrderList.RemoveAt(0);
                    DeliveryManager.Instance.RemoveOrder();
                    found = true;
                    OnOrderSuccess?.Invoke(this, EventArgs.Empty);
                    deliveriesCompleted++;

                }
            }
            if (!found)
            {
                //not a matching recipe 
                plateKitchenObject.DestroySelf();
                OnOrderFail?.Invoke(this, EventArgs.Empty);
            }


        }
    }
    public int GetDeliveredAmount()
    {
        return deliveriesCompleted;
    }
}
