using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CM_ThirdPerson : CharacterMovement
{
    [Header("Third Person Variables"), Space(5f)]
    [SerializeField]
    private float rotationSpeed = 100f;
    [SerializeField]
    private GameObject lookAtPoint;
    float pitch = 0f;
    private InputAction lookInput;

    override protected void Start()
    {
        base.Start();
        lookInput = InputSystem.actions.FindAction("Look");
    }

    override protected void Update()
    {
        base.Update();
        RotatePlayer();
        TiltHead();
    }

    protected override void Walk()
    {
        base.Walk();
        Vector2 inputVector = moveInput.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;
        Vector3 relativeMoveDirection = ((moveDirection.x * controller.transform.right) + (moveDirection.z * controller.transform.forward)).normalized;
        currentHorizontalVelocity = new Vector2(relativeMoveDirection.x, relativeMoveDirection.z);
    }

    private void RotatePlayer()
    {
        Vector2 lookVector = lookInput.ReadValue<Vector2>();
        float yaw = 0;
        if (lookVector.sqrMagnitude > 0.001f)
        {
            yaw += lookVector.x * rotationSpeed * Time.fixedDeltaTime;

            // apply rotation directly
            this.transform.Rotate(Vector3.up * yaw);
        }
    }
    
    private void TiltHead()
    {
        Vector2 lookVector = lookInput.ReadValue<Vector2>();

        if (lookVector.sqrMagnitude > 0.001f)
        {
            // accumulate pitch from input
            pitch -= lookVector.y * rotationSpeed * Time.fixedDeltaTime;
            pitch = math.clamp(pitch, -45f, 45f); // limit pitch to avoid extreme angles

            // apply pitch to lookAtPoint
            lookAtPoint.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}
