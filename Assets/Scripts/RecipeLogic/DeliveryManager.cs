using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    [SerializeField] private float RecipeSpawnTimer = 5f;
    [SerializeField] private int maxOrders = 3;
    [SerializeField] private RecipeListSO recipeListSO;

    public event EventHandler <OnOrderAddedEventArgs> OnOrderAdded;

    public class OnOrderAddedEventArgs : EventArgs { 
        public RecipeSO recipeSO;
    }


    public event EventHandler OnOrderRemoved;
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
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = RecipeSpawnTimer;
                if (currentOrderList.Count < maxOrders)
                {
                    RecipeSO order = recipeListSO.recipeListSOs[UnityEngine.Random.Range(0, recipeListSO.recipeListSOs.Length)];
                    currentOrderList.Add(order);
                    OnOrderAdded?.Invoke(this, new OnOrderAddedEventArgs { recipeSO = order});
                }
            }
        }
       
    }
    public List<RecipeSO> GetCurrentOrderList()
    {
        return currentOrderList;
    }
    public void RemoveOrder()
    {
        OnOrderRemoved?.Invoke(this, EventArgs.Empty);
    }

}
