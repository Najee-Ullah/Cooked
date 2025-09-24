using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
    [SerializeField] IKitchenObjectParent kitchenObjectParent;

    public static void CreateKitchenObject(KitchenObjectSO kSO,IKitchenObjectParent parent)
    {
        if (!parent.HasKitchenObject())
        {
            Transform kitchenObjectInstance = Instantiate(kSO.prefab);
            kitchenObjectInstance.GetComponent<KitchenObject>().SetKitchenObjectParent(parent);
        }
    }

    public KitchenObjectSO getKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent newParent)
    {
        IKitchenObjectParent previousParent = null;
        if (kitchenObjectParent != null)
        {
             previousParent= kitchenObjectParent;
        }
        newParent.SetKitchenObject(this);
        transform.parent = newParent.GetKitchenObjectTransform();
        transform.localPosition = Vector3.zero;
        kitchenObjectParent = newParent;
        if(previousParent != null)
             previousParent.ClearKitchenObject();
    }
    public IKitchenObjectParent GetBaseCounter()
    {
        return kitchenObjectParent;
    }
    public void DestroySelf()
    {
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
}
