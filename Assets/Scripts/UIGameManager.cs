using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    public event Action OnWindowsClosed;

    [SerializeField] private BaseWindow shopWindow;
    [SerializeField] private BaseWindow bankWindow;
    [SerializeField] private BaseWindow cabinWindow;
    
    private void Awake()
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        Player.Instance.OnInteracted += Player_OnInteracted;
        HideWindows();
    }

    private void OnDestroy()
    {
        Player.Instance.OnInteracted -= Player_OnInteracted;
    }

    private void Player_OnInteracted(BaseInteractable interactable)
    {
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

    public void HideWindows()
    {
        shopWindow.gameObject.SetActive(false);
        bankWindow.gameObject.SetActive(false);
        cabinWindow.gameObject.SetActive(false);
        OnWindowsClosed?.Invoke();
    }
}
