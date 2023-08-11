using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    public event Action OnWindowsClosed;

    [SerializeField] private BaseWindow shopWindow;
    [SerializeField] private BaseWindow bankWindow;
    [SerializeField] private BaseWindow cabinWindow;
    [SerializeField] private TextMeshProUGUI goldText;
    
    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnInteractingEnabled += GameManager_OnInteractingEnabled;
        GameManager.Instance.OnGoldAmountChanged += GameManager_OnGoldAmountChanged;
        HideWindows();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnInteractingEnabled -= GameManager_OnInteractingEnabled;
        GameManager.Instance.OnGoldAmountChanged -= GameManager_OnGoldAmountChanged;
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
    }

    public void CloseWindow()
    {
        HideWindows();
        OnWindowsClosed?.Invoke();
    }
}
