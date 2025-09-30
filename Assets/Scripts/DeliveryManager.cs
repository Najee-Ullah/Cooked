using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private float RecipeSpawnTimer = 5f;
    [SerializeField] private int maxOrders = 3;
    [SerializeField] private RecipeListSO recipeListSO;

    public event EventHandler OrderAdded;
    public event EventHandler OrderRemoved;
    private float currentTime;
    public static DeliveryManager Instance;
    private List<RecipeSO> currentOrderList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;


    }

    private void Start()
    {
        currentTime = RecipeSpawnTimer;
        currentOrderList = new List<RecipeSO>();
    }
    private void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = RecipeSpawnTimer;
            if (currentOrderList.Count < maxOrders)
            {
                currentOrderList.Add(recipeListSO.recipeListSOs[UnityEngine.Random.Range(0, recipeListSO.recipeListSOs.Length - 1)]);
                OrderAdded?.Invoke(this, EventArgs.Empty);
            }
        }
        foreach(RecipeSO recipeSO in currentOrderList)
        {
            Debug.Log(recipeSO.name);
        }
    }
    public List<RecipeSO> GetCurrentOrderList()
    {
        return currentOrderList;
    }
    public void RemoveOrder()
    {
        OrderRemoved?.Invoke(this, EventArgs.Empty);
    }

}
