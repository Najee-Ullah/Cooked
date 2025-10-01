
using System;
using System.Data;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public static KitchenGameManager Instance;

    public enum State {
        GameCountDown,
        GamePlaying,
        GameOver
    }
    public float countDownTimer = 5f;
    private float gamePlayTimer = 30f;
    private State state;

    public event EventHandler OnGameOver;

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
                gamePlayTimer -= Time.deltaTime;
                if(gamePlayTimer <= 0)
                {
                    state = State.GameOver;
                    OnGameOver?.Invoke(this, EventArgs.Empty);
                }
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
}