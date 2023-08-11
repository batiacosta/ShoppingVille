using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public event Action<BaseInteractable> OnSelectedInteractableChanged;
    public event Action<BaseInteractable> OnInteracted; 
    
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private Animator animator = null;
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private PlayerOutfitManager playerOutfitManager = null;
    [SerializeField] private PlayerInventoryManager playerInventoryManager;

    [SerializeField] private ContactFilter2D contactFilter2D;
    

    private Rigidbody2D _rigidbody;
    private PlayerInput _playerInput;
    private Vector2 _inputDirection;
    private Vector2 _lastDirection;
    private BaseInteractable _currentInteractable;
    private bool _isInteracting = false;
    private bool _canMove = true;
    private Transform _nativeTransform;
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _nativeTransform = GetComponent<Transform>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        playerInventoryManager.InventorySetUp();
        playerOutfitManager.SetOutfit();
    }

    private void GameManager_OnStateChanged()
    {
        _canMove = GameManager.Instance.IsStatePlaying();
        _isInteracting = !_canMove;
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            Move();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnStateChanged -= GameManager_OnStateChanged;
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
            if (_inputDirection.x < 0)
            {
                transform.rotation = Quaternion.AngleAxis(-180, Vector3.up);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
        }
        else
        {
            couldMove = false;
        }
        animator.SetBool(IsWalking, couldMove);
        
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

    private void SetCurrentInteractable(BaseInteractable interactable)
    {
        //if (!interactable.CanInteract()) return;
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
        if (_isInteracting) return;
        if (_currentInteractable == null) return;
        if(!_currentInteractable.CanInteract()) return;
        _currentInteractable.Interact();
        _isInteracting = true;
        _canMove = false;
        OnInteracted?.Invoke(_currentInteractable);
    }

    public void SetOutfit()
    {
        playerOutfitManager.SetOutfit();
    }

    public InventorySO GetPlayerInventory() => playerInventoryManager.GetInventory();
    
    
    
    public BaseInteractable GetCurrentInteractable() => _currentInteractable;


}
