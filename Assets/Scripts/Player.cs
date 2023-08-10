using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public event Action<IInteractable> OnSelectedInteractableChanged;
    
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float interactionRange = 2f;

    [SerializeField] private ContactFilter2D contactFilter2D;
    

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private Vector2 _inputDirection;
    private Vector2 _lastDirection;
    private BaseInteractable _currentInteractable;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
        //HandleInteractions();
    }

    

    private void Move()
    {
        bool couldMove = false;
        if (_inputDirection != Vector2.zero)
        {
            couldMove = TryMove(_inputDirection);
            _lastDirection = _inputDirection;
            if (!couldMove)
            {
                couldMove = TryMove(new Vector2(_inputDirection.x, 0));
                _lastDirection = new Vector2(_inputDirection.x, 0);
                if (!couldMove)
                {
                    couldMove = TryMove(new Vector2(0, _inputDirection.y));
                    _lastDirection = new Vector2(0, _inputDirection.y);
                }
            }
        }
        else
        {
            couldMove = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out BaseInteractable interactable))
        {
            SetCurrentInteractable(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out BaseInteractable interactable))
        {
            SetCurrentInteractable(null);
        }
    }

    private void HandleInteractions()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _inputDirection, interactionRange);
        Debug.DrawLine(transform.position, hit.normal, Color.yellow);
        if (hit.collider.TryGetComponent(out BaseInteractable interactable))
        {
            SetCurrentInteractable(interactable);
        }
    }

    private void SetCurrentInteractable(BaseInteractable interactable)
    {
        _currentInteractable = interactable;
        OnSelectedInteractableChanged?.Invoke(_currentInteractable);
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
            int collisionCount = _rigidbody.Cast(
                direction, 
                contactFilter2D, 
                castCollisions, 
                movementSpeed * Time.fixedDeltaTime
            );
        
            if (collisionCount != 0) return false;
        
            Vector2 movementDirection = direction * movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + movementDirection);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnMove(InputValue inputValue)
    {
        _inputDirection = inputValue.Get<Vector2>();
    }

    public void OnInteract(InputValue inputValue)
    {
        if (_currentInteractable == null) return;
        _currentInteractable.Interact();
    }
    
    
}
