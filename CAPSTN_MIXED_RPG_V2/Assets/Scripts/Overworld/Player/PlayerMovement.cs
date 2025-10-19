using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [Header("Player Movement Variables")]
    [SerializeField, Space(10f)]
    private GameObject playerModel;

    public bool IsMovementEnabled = true;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void LateUpdate()
    {
        if (!IsMovementEnabled) return;

        base.LateUpdate();
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 movementDirection = new Vector3(currentHorizontalVelocity.x, 0f, currentHorizontalVelocity.y);

        if (movementDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            playerModel.transform.rotation = Quaternion.Slerp(
                playerModel.transform.rotation,
                targetRotation,
                Time.deltaTime * 10f // Adjust 10f for rotation speed
            );
        }
    }
}
