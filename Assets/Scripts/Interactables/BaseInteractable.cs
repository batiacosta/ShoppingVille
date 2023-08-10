using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
        Debug.Log($"Interacting with {gameObject.name}");
    }
}
