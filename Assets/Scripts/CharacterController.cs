using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D _rBody;

    private Animator _animator;

    private InputAction _moveAction;
    private InputAction _jumpAction;

    private float _movementSpeed = 8f;
    private float _jumpForce = 6f;

    [SerializeField]private Transform _groundSensor;
    private float _groundRadius = 0.5f;

    private Vector2 _moveInput;


    void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();

        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();

        if(_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            _animator.SetBool("IsRunning", true);
        }

        else if(_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            _animator.SetBool("IsRunning", true);
        }

        else
        {
            _animator.SetBool("IsRunning", false);
        }
        
        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
        _animator.SetBool("IsJumping", !IsGrounded());
    }

    void FixedUpdate()
    {
        _rBody.linearVelocityX = _moveInput.x * _movementSpeed;
    }

    void Jump()
    {
        _rBody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(_groundSensor.position, _groundRadius);

        foreach(Collider2D item in collider2D)
        {
            if(item.gameObject.layer == 3)
            {
                return true;
            }

        }

        return true;
    }
}