using UnityEditor;
using UnityEngine;

public struct PlayerOverlapResult
{
    public bool IsOverlapping;
    public IInteractable OverlapObject;
}

public class PlayerModel : MonoBehaviour
{
    public IInteractable TargetInteractable;
    public bool IsOverlapping = false;

    [SerializeField]
    private float rayLength = 0;
    [SerializeField]
    private PlayerMovement movement;

    public void UpdateTargetObject(IInteractable _targetObject)
    {
        TargetInteractable = _targetObject;
    }

    public PlayerOverlapResult CheckForTargetObject(Transform _rayTransformOrigin)
    {
        RaycastHit hit;
        PlayerOverlapResult result = new PlayerOverlapResult();

        if (Physics.Raycast(_rayTransformOrigin.position, _rayTransformOrigin.TransformDirection(Vector3.forward), out hit, rayLength))
        {
            if (hit.transform.gameObject.TryGetComponent<IInteractable>(out var interactable))
            {
                result.IsOverlapping = true;
                result.OverlapObject = hit.transform.gameObject.GetComponent<IInteractable>();
            }
        }

        return result;
    }

    // For Debugging purposes.
    public void DrawCheckerRay(Transform _rayTransformOrigin)
    {
        Debug.DrawRay(_rayTransformOrigin.position, _rayTransformOrigin.TransformDirection(Vector3.forward) * rayLength, Color.aliceBlue);
    }

    public void Interact()
    {
        if (TargetInteractable != null)
        {
            TargetInteractable.Interact();
        }
    }

    public void SetStateMovement(bool _state)
    {
        movement.IsMovementEnabled = _state;
    }
}
