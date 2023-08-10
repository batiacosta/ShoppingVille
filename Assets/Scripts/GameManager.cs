using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action OnStateChanged ;

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

    private void Start()
    {
        Player.Instance.OnInteracted += Player_OnInteracted;
        UIGameManager.Instance.OnWindowsClosed += UIGameManager_OnWindowsClosed;
        SetGameState(State.GamePlaying);
    }
    private void OnDestroy()
    {
        Player.Instance.OnInteracted -= Player_OnInteracted;
        UIGameManager.Instance.OnWindowsClosed -= UIGameManager_OnWindowsClosed;
    }

    private void UIGameManager_OnWindowsClosed()
    {
        SetGameState(State.GamePlaying);
    }

    private void SetGameState(State state)
    {
        _state = state;
        OnStateChanged?.Invoke();
    }

    private void Player_OnInteracted(BaseInteractable interactable)
    {
        SetGameState(State.Interacting);
    }

    public bool IsStatePlaying() => _state == State.GamePlaying;

    
}
