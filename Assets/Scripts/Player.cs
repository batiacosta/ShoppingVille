using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private Animator animator = null;

    [SerializeField] private ContactFilter2D contactFilter2D;
    

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private Vector2 _inputDirection;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    
    private void Move()
    {
        bool couldMove = false;
        if (_inputDirection != Vector2.zero)
        {
            couldMove = TryMove(_inputDirection);

            if (!couldMove)
            {
                couldMove = TryMove(new Vector2(_inputDirection.x, 0));
                if (!couldMove)
                {
                    couldMove = TryMove(new Vector2(0, _inputDirection.y));
                }
            }
        }
        else
        {
            couldMove = false;
        }
        
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
    
    
}
