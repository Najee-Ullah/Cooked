using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [Header("HeaderPositions")]
    [SerializeField] Transform CustomerInstantiateTransform;
    [SerializeField] Transform CustomerTargetTransform;
    [SerializeField] Transform CustomerDestroyeTransform;
    [Header("Settings")]
    [SerializeField] Transform CustomerPrefab;
    [SerializeField] private float queueSpacing = 2f,customerMoveSpeed = 4f;
    [SerializeField] private int customerPoolSize = 4;
    private List<Customer> customers;
    private ObjectPool<Customer> customerPool;

    private void Start()
    {
        customers = new List<Customer>();
        customerPool = new ObjectPool<Customer>(CustomerPrefab.GetComponent<Customer>(),customerPoolSize,transform);

        DeliveryManager.Instance.OnOrderAdded += Instance_OnOrderAdded;
        DeliveryManager.Instance.OnOrderRemoved += Instance_OnOrderRemoved;
    }

    private void Instance_OnOrderAdded(object sender, DeliveryManager.OnOrderAddedEventArgs e)
    {
        //spawn customer
        Customer customer = customerPool.Get();
        customer.transform.position = CustomerInstantiateTransform.position;
        //configure customer
        customer.SetupCustomer(e.recipeSO,customerMoveSpeed);
        customer.OnCustomerDisable += Customer_OnCustomerDisable; ;
        customer.ReachTarget(CustomerTargetTransform.position);

        customers.Add(customer);

        //move cll customers
        UpdateCustomerQueuePositions();
    }

    private void Customer_OnCustomerDisable(Customer obj)
    {
        obj.OnCustomerDisable -= Customer_OnCustomerDisable;
        customerPool.ReturnToPool(obj);
    }

    private void Instance_OnOrderRemoved(object sender, System.EventArgs e)
    {
        if (customers.Count == 0) return;

        // front customer leaves
        Customer firstCustomer = customers[0];
        customers.RemoveAt(0);
        firstCustomer.DestroySelf(CustomerDestroyeTransform);

        // remaining customers update position
        UpdateCustomerQueuePositions();
    }

    private void UpdateCustomerQueuePositions()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            // equally spaced customer queue
            Vector3 queuePos = CustomerTargetTransform.position + (CustomerTargetTransform.right * queueSpacing * i);
            customers[i].ReachTarget(queuePos);
        }
    }
}