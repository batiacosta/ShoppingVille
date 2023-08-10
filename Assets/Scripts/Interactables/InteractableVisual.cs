using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] highlightObjects;
    [SerializeField] private BaseInteractable _interactableParent;

    private void Start()
    {
        Hide();
        Player.Instance.OnSelectedInteractableChanged += Player_OnSelectedInteractableChanged;
    }

    private void OnDestroy()
    {
        Player.Instance.OnSelectedInteractableChanged -= Player_OnSelectedInteractableChanged;
    }

    private void Player_OnSelectedInteractableChanged(IInteractable interactable)
    {
        if (_interactableParent == interactable)
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
}
