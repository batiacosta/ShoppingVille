using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shop : BaseInteractable
{
    private bool _canInteract = false;
    public override void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }

    public void DisplayCanInteract()
    {
        _canInteract = true;
    }
    
}
