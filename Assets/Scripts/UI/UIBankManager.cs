using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBankManager : MonoBehaviour
{
    [SerializeField] private Image character = null;
    [SerializeField] private List<Sprite> characterStates;
    [SerializeField] private TextMeshProUGUI textInteraction;
    [SerializeField] private Image roundedDelay;
    [SerializeField] private Button askForMoneyButton;
    [SerializeField] private BankSystem bankSystem;
    [SerializeField] private List<string> textOutputs;

    private int _maxTries = 2;
    private float _maxTime = 5;
    private float _currentTime;
    private int _currentTry;
    private bool _isDelayEnabled;
    private enum State
    {
        Start, Rejected, GiveMoney, Failed
    }
    private State _state;
    private Dictionary<State, string> _textOutputDictionary;
    
    private void OnEnable()
    {
        _currentTry = 0;
        _currentTime = 0;
        SetState(State.Start);
    }

    private void OnDisable()
    {
        character.sprite = characterStates[0];
    }

    private void Update()
    {
        if (_isDelayEnabled)
        {
            SetText(textOutputs[1]);
            roundedDelay.gameObject.SetActive(_isDelayEnabled);
            UpdateDelayNextTry();
        }
        
    }

    private void UpdateDelayNextTry()
    {
        if (_currentTime < _maxTime)
        {
            _currentTime += Time.deltaTime;
            roundedDelay.fillAmount = _currentTime / _maxTime;
        }
        else
        {
            SetState(State.Start);
        }
    }

    private void SetState(State state)
    {
        _state = state;
        switch (_state)
        {
            case State.Start:
                _isDelayEnabled = false;
                character.sprite = characterStates[0];
                roundedDelay.gameObject.SetActive(_isDelayEnabled);
                askForMoneyButton.gameObject.SetActive(true);
                SetText(textOutputs[0]);
                break;
            case State.Rejected:
                _isDelayEnabled = true;
                _currentTime = 0;
                character.sprite = characterStates[1];
                roundedDelay.gameObject.SetActive(true);
                askForMoneyButton.gameObject.SetActive(false);
                SetText(textOutputs[1]);
                break;
            case State.GiveMoney:
                _isDelayEnabled = false;
                character.sprite = characterStates[0];
                roundedDelay.gameObject.SetActive(false);
                askForMoneyButton.gameObject.SetActive(false);
                break;
            case State.Failed:
                _isDelayEnabled = false;
                character.sprite = characterStates[1];
                roundedDelay.gameObject.SetActive(false);
                askForMoneyButton.gameObject.SetActive(false);
                SetText(textOutputs[3]);
                break;
        }
    }

    private void SetText(string output)
    {
        textInteraction.text = output;
    }

    public void AskForMoney()
    {
        _currentTry++;
        if (_currentTry <= _maxTries)
        {
            int money = bankSystem.TryAskForMoney();
            if (money == 0)
            {
                SetState(State.Rejected);
            }
            else
            {
                SetState(State.GiveMoney);
                SetText($"{textOutputs[2]} ${money}");
                GameManager.Instance.AddGold(money);
                _currentTry = 0;
            }
        }
        else
        {
            SetState(State.Failed);
        }
    }
}
