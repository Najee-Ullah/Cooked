
using System;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance {get;private set; }

    public event EventHandler OrderSuccess;
    public event EventHandler OrderFail;

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
        if(player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            List<KitchenObjectSO> kSOList = plateKitchenObject.GetKitchenObjectSOList();
            List<RecipeSO> currentOrderList = DeliveryManager.Instance.GetCurrentOrderList();
            bool found = false;
            for(int i = 0;i<currentOrderList.Count;i++)
            {
                if (kSOList.Count == currentOrderList[i].kitchenObjectSOs.Count)
                {
                    bool correctOrder = true;
                    for(int j =0;j< kSOList.Count; j++)
                    {
                        if (currentOrderList[i].kitchenObjectSOs.Contains(kSOList[j]))
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
                        currentOrderList.RemoveAt(i);
                        DeliveryManager.Instance.RemoveOrder();
                        found = true;
                        OrderSuccess?.Invoke(this, EventArgs.Empty);
                        deliveriesCompleted++;

                        break;
                    }
                }
            }
            if(!found)
            {
                //no matching recipe found
                plateKitchenObject.DestroySelf();
                OrderFail?.Invoke(this, EventArgs.Empty);
            }

        }
    }
    public int GetDeliveredAmount()
    {
        return deliveriesCompleted;
    }
}
