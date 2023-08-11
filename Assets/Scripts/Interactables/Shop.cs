using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Shop : BaseInteractable
{
    private void Start()
    {
        _sleepingTime = 8f;
    }
    public override void Interact()
    {
        
    }

    public void DisplayCanInteract()
    {
        _canInteract = true;
    }
    
}
