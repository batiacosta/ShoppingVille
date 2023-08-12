using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    public event Action OnWindowsClosed;

    [SerializeField] private BaseWindow shopWindow;
    [SerializeField] private BaseWindow bankWindow;
    [SerializeField] private BaseWindow cabinWindow;
    [SerializeField] private UIGameOver gameOverWindow;
    [SerializeField] private UIGamePaused pauseGameWindow;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Image clock;
    
    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnInteractingEnabled += GameManager_OnInteractingEnabled;
        GameManager.Instance.OnGoldAmountChanged += GameManager_OnGoldAmountChanged;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        HideWindows();
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnInteractingEnabled -= GameManager_OnInteractingEnabled;
        GameManager.Instance.OnGoldAmountChanged -= GameManager_OnGoldAmountChanged;
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged()
    {
        if (GameManager.Instance.IsGamePaused())
        {
            pauseGameWindow.gameObject.SetActive(true);
        }

        if (GameManager.Instance.IsGameOver())
        {
            HideWindows();
            gameOverWindow.gameObject.SetActive(true);
        }
    }

    private void GameManager_OnGoldAmountChanged(int amount)
    {
        UpdateGold(amount);
    }

    private void GameManager_OnInteractingEnabled(BaseInteractable interactable)
    {
        if (!interactable.CanInteract()) return;

        switch (interactable)
        {
            case Shop:
                HideWindows();
                shopWindow.gameObject.SetActive(true);
                break;
            case Bank:
                HideWindows();
                bankWindow.gameObject.SetActive(true);
                break;
            case Cabin:
                HideWindows();
                cabinWindow.gameObject.SetActive(true);
                break;
        }
    }

    private void UpdateGold(int amount)
    {
        goldText.text = amount.ToString();
    }

    public void HideWindows()
    {
        shopWindow.gameObject.SetActive(false);
        bankWindow.gameObject.SetActive(false);
        cabinWindow.gameObject.SetActive(false);
        gameOverWindow.gameObject.SetActive(false);
    }

    public void CloseWindow()
    {
        HideWindows();
        OnWindowsClosed?.Invoke();
    }

    public void UpdateClock(float fillValue)
    {
        clock.fillAmount = fillValue;
    }
}
