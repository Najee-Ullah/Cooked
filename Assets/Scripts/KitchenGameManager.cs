
using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance;

    [SerializeField] InputSystem InputSystem;

    private const string HIGHESTDELIVERIESMADE = "HighestDeliveriesMade";

    public enum State {
        GameToStart,
        GameCountDown,
        GamePlaying,
        GameOver,
        GamePaused
    }
    [SerializeField] private float countDownTimer = 5f;
    private float elapsedTime;

    [SerializeField] private float gamePlayTimer = 30f;
    [SerializeField] private float gameplayBonus = 10f;
    private State state;

    public event EventHandler OnGameOver;
    public event EventHandler <OnGameSateChangedEventArgs> OnGameStateChanged;
    public event EventHandler <OnCountChangedEventArgs> OnCountChanged;

    public class OnCountChangedEventArgs : EventArgs
    {
        public int count;
    }

    public class OnGameSateChangedEventArgs : EventArgs
    {
        public State currentState;
    }


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
        elapsedTime = countDownTimer+1;

        state = State.GameToStart;
        InputSystem.OnPauseAction += InputSystem_OnPauseAction;
        DeliveryCounter.Instance.OnOrderSuccess += Instance_OrderSuccess;
    }

    private void Instance_OrderSuccess(object sender, EventArgs e)
    {
        IncreaseTime();
    }

    private void InputSystem_OnPauseAction(object sender, EventArgs e)
    {
            TogglePause();
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.GameToStart:
                if (Input.anyKeyDown)
                {
                    state = State.GameCountDown;
                    OnGameStateChanged?.Invoke(this, new OnGameSateChangedEventArgs { currentState = state });
                }
                break;
            case State.GameCountDown:
                countDownTimer -= Time.deltaTime;
                if(Mathf.CeilToInt(countDownTimer)!= Mathf.CeilToInt(elapsedTime))
                {
                    elapsedTime = Mathf.CeilToInt(countDownTimer);
                    OnCountChanged?.Invoke(this, new OnCountChangedEventArgs { count = Mathf.CeilToInt(countDownTimer) });
                }
                if(countDownTimer <= 0)
                {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                Time.timeScale = 1;
                gamePlayTimer -= Time.deltaTime;
                if(gamePlayTimer <= 0)
                {
                    state = State.GameOver;
                    OnGameOver?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePaused:
                Time.timeScale = 0;
                break;
            case State.GameOver:
                if (PlayerPrefs.HasKey(HIGHESTDELIVERIESMADE))
                {
                    if (PlayerPrefs.GetInt(HIGHESTDELIVERIESMADE) < DeliveryCounter.Instance.GetDeliveredAmount())
                    {
                        PlayerPrefs.SetInt(HIGHESTDELIVERIESMADE, DeliveryCounter.Instance.GetDeliveredAmount());
                        PlayerPrefs.Save();
                    }
                }
                else
                {
                    PlayerPrefs.SetInt(HIGHESTDELIVERIESMADE, DeliveryCounter.Instance.GetDeliveredAmount());
                    PlayerPrefs.Save();
                }
                    break;
        }
        
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public float GetGamePlayTimer()
    {
        return gamePlayTimer;
    }
    public bool isGameOver()
    {
        return state == State.GameOver;
    }

    public void TogglePause()
    {
        if (state == State.GamePaused)
        {
            state = State.GamePlaying;
            OnGameStateChanged?.Invoke(this, new OnGameSateChangedEventArgs { currentState = state });
        }
        else if(state == State.GamePlaying)
        {
            state = State.GamePaused;
            OnGameStateChanged?.Invoke(this, new OnGameSateChangedEventArgs { currentState = state });
        }
    }
    public bool isGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public void IncreaseTime()
    {
        gamePlayTimer += gameplayBonus;
    }
}