using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CM_TopDown : CharacterMovement
{
    private InputAction pointerInput;
    [SerializeField]
    private bool followPointer;

    protected override void Start()
    {
        base.Start();
        pointerInput = InputSystem.actions.FindAction("Point");
    }

    protected override void Update()
    {
        base.Update();

        if (followPointer)
        {
            LookAtPointer();
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    private void LookAtPointer()
    {
        Vector3 direction = this.transform.position - GetPointerWorldPresicion();
        direction = new Vector3(direction.x, 0f, direction.z);
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        this.transform.rotation = rotation;
    }

    private Vector3 GetPointerWorldPresicion()
    {
        Vector2 screenPosition = pointerInput.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }

        return Vector3.right;
    }
}
