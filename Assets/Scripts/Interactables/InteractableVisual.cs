using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InteractableVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] highlightObjects;
    [SerializeField] private Image sleepingIndicator;
    [SerializeField] private BaseInteractable interactableParent;

    private float _sleepingTime;
    private float _currentSleepingTime = 0f;

    private void Start()
    {
        Hide();
        Player.Instance.OnSelectedInteractableChanged += Player_OnSelectedInteractableChanged;
        interactableParent.OnSleepStarted += BaseInteractable_OnSleepStarted;
        sleepingIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        UpdateSleepingIndicator();
    }
    
    private void OnDestroy()
    {
        Player.Instance.OnSelectedInteractableChanged -= Player_OnSelectedInteractableChanged;
        interactableParent.OnSleepStarted -= BaseInteractable_OnSleepStarted;
    }

    private void BaseInteractable_OnSleepStarted()
    {
        _sleepingTime = interactableParent.GetSleepingTime();
        _currentSleepingTime = 0;
        sleepingIndicator.gameObject.SetActive(true);
    }

    private void Player_OnSelectedInteractableChanged(BaseInteractable interactable)
    {
        if (interactableParent == interactable)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject highlightObject in highlightObjects)
        {
            highlightObject.gameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject highlightObject in highlightObjects)
        {
            highlightObject.gameObject.SetActive(false);
        }
    }
    
    private void UpdateSleepingIndicator()
    {
        if (!interactableParent.CanInteract())
        {
            if (_currentSleepingTime < _sleepingTime)
            {
                _currentSleepingTime += Time.deltaTime;
                sleepingIndicator.fillAmount = _currentSleepingTime / _sleepingTime;
            }
            else
            {
                _currentSleepingTime = 0;
                interactableParent.SetCanInteract(true);
                sleepingIndicator.gameObject.SetActive(false);
            }
        }
    }
}
