using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    public event Action OnSleepStarted;
    protected float _sleepingTime;
    protected bool _canInteract;
    public virtual void Interact()
    {
        
    }
    
    public virtual void Sleep()
    {
        _canInteract = false;
        Debug.Log($"Interctable sleeping is {gameObject.name}");
        OnSleepStarted?.Invoke();
    }

    public bool CanInteract() => _canInteract;

    public void SetCanInteract(bool canInteract)
    {
        _canInteract = canInteract;
    }

    public float GetSleepingTime() => _sleepingTime;
}
