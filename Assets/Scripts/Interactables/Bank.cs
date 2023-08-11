using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : BaseInteractable
{
    private void Start()
    {
        _sleepingTime = 10f;
    }

    

    public override void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }
    
}
