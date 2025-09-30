
using System;
using System.Collections.Generic;

public class DeliveryCounter : BaseCounter
{

    public override void Interact(Player player)
    {
        if(player.GetKitchenObject().tryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            List<KitchenObjectSO> kSOList = plateKitchenObject.GetKitchenObjectSOList();
            List<RecipeSO> currentOrderList = DeliveryManager.Instance.GetCurrentOrderList();
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
                        //kSOList[j]
                    }
                    if (correctOrder)
                    {
                        plateKitchenObject.DestroySelf();
                        currentOrderList.RemoveAt(i);
                        DeliveryManager.Instance.RemoveOrder();
                        break;
                    }
                }
            }

        }
    }
}
