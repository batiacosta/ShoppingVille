using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnStateChanged;
    public event Action<BaseInteractable> OnInteractingEnabled;
    public event Action<int> OnGoldAmountChanged;

    private int _gold = 1000;
    private float _maxTime = 60f;
    private float _currentTime = 0;
    private float _goldReducerPeriod = 1f;
    private float _currentGoldTime;
    private int _goldChunk = 25;

    public enum State
    {
        WaitingToStart,
        GamePlaying,
        Interacting,
        PausedGame,
        GameOver
    }

    public enum GameOverStatus
    {
        GotBroken, TimeFinished
    }


    private GameOverStatus _gameOverStatus;
    private State _state;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private BaseInteractable _currentActiveInteractable;

    private void Start()
    {
        _currentTime = 0;
        _currentGoldTime = 0;
        _gameOverStatus = GameOverStatus.GotBroken;
        Player.Instance.OnInteracted += Player_OnInteracted;
        UIGameManager.Instance.OnWindowsClosed += UIGameManager_OnWindowsClosed;
        SetGameState(State.WaitingToStart);
    }

    private void Update()
    {
        if (_state == State.GamePlaying || _state == State.Interacting)
        {
            ReduceGoldByTime();
            UpdateGameTime();
        }
    }

    private void ReduceGoldByTime()
    {
        if (_currentGoldTime <= _goldReducerPeriod)
        {
            _currentGoldTime += Time.deltaTime;
        }
        else
        {
            AddGold(-Mathf.RoundToInt(_currentGoldTime * _goldChunk));
            _currentGoldTime = 0;
        }
    }

    private void UpdateGameTime()
    {
        if (_currentTime <= _maxTime)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            _gameOverStatus = GameOverStatus.TimeFinished;
            SetGameState(State.GameOver);
        }
        UIGameManager.Instance.UpdateClock(_currentTime / _maxTime);
    }

    private void OnDestroy()
    {
        Player.Instance.OnInteracted -= Player_OnInteracted;
        UIGameManager.Instance.OnWindowsClosed -= UIGameManager_OnWindowsClosed;
    }

    private void UIGameManager_OnWindowsClosed()
    {
        if (_currentActiveInteractable != null)
        {
            _currentActiveInteractable.Sleep();
        }
        SetGameState(State.GamePlaying);
    }

    private void SetGameState(State state)
    {
        _state = state;
        OnStateChanged?.Invoke();
    }

    private void Player_OnInteracted(BaseInteractable interactable)
    {
        _currentActiveInteractable = interactable;
        SetGameState(State.Interacting);
        OnInteractingEnabled?.Invoke(interactable);
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        
        if (_gold <= 0)
        {
            _gold = 0;
            _gameOverStatus = GameOverStatus.GotBroken;
            SetGameState(State.GameOver);
        }
        OnGoldAmountChanged?.Invoke(_gold);
    }

    public void SetPlay()
    {
        SetGameState(State.GamePlaying);
    }
    

    public bool IsStatePlaying() => _state == State.GamePlaying;
    public bool IsGamePaused() => _state == State.PausedGame;
    public bool IsGameOver() => _state == State.GameOver;

    public bool DidLose() => _gameOverStatus == GameOverStatus.GotBroken;
    public int GetGold() => _gold;
    public BaseInteractable GetActiveInteractable() => _currentActiveInteractable;


}
