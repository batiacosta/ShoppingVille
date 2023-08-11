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
    private float _maxTime = 120f;
    private float _currentTime = 0;

    public enum State
    {
        WaitingToStart,
        GamePlaying,
        Interacting,
        PausedGame,
        GameOver
    }

    private State _state;
    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private BaseInteractable _currentActiveInteractable;

    private void Start()
    {
        Player.Instance.OnInteracted += Player_OnInteracted;
        UIGameManager.Instance.OnWindowsClosed += UIGameManager_OnWindowsClosed;
        SetGameState(State.GamePlaying);
    }

    private void Update()
    {
        if (_state == State.GamePlaying)
        {
            //  Reduce gold every second
        }
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
            // Game over
            _gold = 0;
        }
        OnGoldAmountChanged?.Invoke(_gold);
    }

    public bool IsStatePlaying() => _state == State.GamePlaying;
    public int GetGold() => _gold;
    public BaseInteractable GetActiveInteractable() => _currentActiveInteractable;


}
