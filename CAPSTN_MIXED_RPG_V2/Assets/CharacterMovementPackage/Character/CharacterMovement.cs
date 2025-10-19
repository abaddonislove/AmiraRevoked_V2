using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// README:
/// 
/// For this script requires custom layers: Ground
/// Ground layer is applied to anything that would be considered a walkable ground.
/// This allows the player to jump on named object. Objects without the layer ground will make the player continue to fall.
/// This is not ideal and can be made better. One solution could be to give non-walkable/ non-jumpable a custom layer which the CheckBox() will exempt.
/// 
/// This also requires the player to have a 'Ground Target Point'. This is a GameObject that is the point of origin for the CheckBox() used for ground checking.
/// Current layout for the CheckBox() looks for 'Ground' Layer.
/// </summary>

public class CharacterMovement : MonoBehaviour
{
    #region Variables
    [Header("Gravity Settings")]
    [SerializeField]
    private float gravityScale = 10f;
    [SerializeField]
    private float maxDropSpeed = 20f;

    [Header("Ground Checker"), Space(5f)]
    [SerializeField]
    protected GameObject groundCastPoint;
    [SerializeField]
    private Vector3 boxExtents;

    // Movement Variables
    [Header("Movement Variables"), Space(5f)]
    [SerializeField]
    private float jumpHeight = 20f;
    [SerializeField]
    private float jumpCutMultiplier = 0.75f;
    [SerializeField]
    private float jumpBufferDuraction = 0.2f;
    [SerializeField]
    private bool canJump = true;

    [Space(2.5f)]
    [SerializeField]
    private float moveSpeed = 5f;
    protected CharacterController controller;
    [SerializeField]
    protected float currentVelocityY;
    [SerializeField]
    protected Vector2 currentHorizontalVelocity;

    // Input Actions
    protected InputAction moveInput;
    protected InputAction jumpInput;

    // Visual Checker
    [Space(5f)]
    [SerializeField]
    private bool isAirborne;

    [Header("Events"), Space(5f)]
    public UnityEvent OnGrounded;

    // Coroutines
    protected Coroutine jumpBufferCoroutine;
    #endregion

    virtual protected void Start()
    {
        // Component setup
        controller = this.GetComponent<CharacterController>();

        // Input action setup
        moveInput = InputSystem.actions.FindAction("Move");
        jumpInput = InputSystem.actions.FindAction("Jump");

        // Input action event bind
        jumpInput.started += context => TryJump();
        jumpInput.canceled += context => CutJump();

        // Event setup
        OnGrounded.AddListener(ResetDrop);
    }

    virtual protected void Update()
    {
        if (!IsGrounded())
        {
            isAirborne = true;
        }

        if (IsGrounded() && isAirborne)
        {
            OnGrounded.Invoke();
        }

        //Debug.Log(currentVelocityY);
    }

    virtual protected void LateUpdate()
    {
        Move();

        if (!IsGrounded())
        {
            ApplyGravity();
        }
    }

    private void Move()
    {
        // Horizontal Movement
        Walk();
        Vector3 horizontalMovementDirection = new Vector3(currentHorizontalVelocity.x, 0f, currentHorizontalVelocity.y);

        // Vertical Movement
        Vector3 verticalMovementDireciton = new Vector3(0f, currentVelocityY, 0f);

        controller.Move(horizontalMovementDirection * moveSpeed * Time.deltaTime + verticalMovementDireciton * Time.deltaTime);
    }

    virtual protected void Walk()
    {
        Vector2 inputVector = moveInput.ReadValue<Vector2>();
        currentHorizontalVelocity = new Vector2(inputVector.x, inputVector.y).normalized;
    }

    private void TryJump()
    {
        // Boolean switch for allowing jump.
        if (!canJump) return;

        if (jumpBufferCoroutine == null)
        {
            jumpBufferCoroutine = StartCoroutine(CO_JumpBuffer());
        }
        else
        {
            StopCoroutine(jumpBufferCoroutine);
        }

        
    }

    private void Jump()
    {
        currentVelocityY = CalculateJumpPower(jumpHeight);
    }

    private void CutJump()
    {
        if (currentVelocityY > 0)
        {
            currentVelocityY *= jumpCutMultiplier;
        }
    }

    private float CalculateJumpPower(float _height)
    {
        return math.sqrt(2 * _height * gravityScale);
    }

    private void ApplyGravity()
    {
        currentVelocityY -= gravityScale * Time.deltaTime;
        currentVelocityY = math.clamp(currentVelocityY, -maxDropSpeed, 99999);
    }

    private bool IsGrounded()
    {
        return Physics.CheckBox(groundCastPoint.transform.position, boxExtents, Quaternion.identity, LayerMask.GetMask("Ground"));
    }

    private void ResetDrop()
    {
        // Keeps character from jumping off slope
        currentVelocityY = -4.5f;
        // Used for sending Grounded event
        isAirborne = false;
    }

    private IEnumerator CO_JumpBuffer()
    {
        float currentTime = Time.time;
        float endTime = currentTime + jumpBufferDuraction;

        while (Time.time < endTime)
        {
            if (IsGrounded())
            {
                Jump();
                jumpBufferCoroutine = null; // clear reference
                yield break;
            }
            yield return null;
        }
        jumpBufferCoroutine = null;
    }
}
