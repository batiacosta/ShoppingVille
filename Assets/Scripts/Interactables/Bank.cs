using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : BaseInteractable
{
    public override void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }
}
