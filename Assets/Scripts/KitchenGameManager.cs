
using System;
using System.Data;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance;

    [SerializeField] InputSystem InputSystem;

    public enum State {
        GameCountDown,
        GamePlaying,
        GameOver,
        GamePaused
    }
    public float countDownTimer = 5f;
    private float gamePlayTimer = 30f;
    private State state;

    public event EventHandler OnGameOver;
    public event EventHandler <OnGameSateChangedEventArgs >OnGameStateChanged;
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
        state = State.GameCountDown;
        InputSystem.OnPauseAction += InputSystem_OnPauseAction;
    }

    private void InputSystem_OnPauseAction(object sender, EventArgs e)
    {
            TogglePause();
    }

    private void Update()
    {
        switch (state)
        { 
            case State.GameCountDown:
                countDownTimer -= Time.deltaTime;
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
                break;
        }
        
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public float GetCountDownTimer()
    {
        return countDownTimer;
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
}