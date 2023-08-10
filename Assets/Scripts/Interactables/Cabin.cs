using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabin : BaseInteractable
{
    public override void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }
}
