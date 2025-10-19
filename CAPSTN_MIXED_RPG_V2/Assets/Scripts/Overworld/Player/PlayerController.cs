using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerModel model;
    [SerializeField]
    private PlayerView view;

    private InputAction interactInput;

    public event Action<IInteractable> OnBeginOverlapInteractable;
    public event Action OnExitOverlapInteractable;

    void Awake()
    {
        OnBeginOverlapInteractable += FillTargetObject;
        OnExitOverlapInteractable += ClearTargetObject;
    }

    void Start()
    {
        GameManager.Instance.OnGamePause += PauseMovement;
        GameManager.Instance.OnGameResume += ResumeMovement;
        InitializeInputActions();
    }

    private void Update()
    {
        MonitorOverlap();
    }

    private void InitializeInputActions()
    {
        interactInput = InputSystem.actions.FindAction("Interact");
        interactInput.performed += ctx => Interact();
    }

    private void MonitorOverlap()
    {
        Transform rayOriginTransform = view.GetPlayerModel().transform;
        PlayerOverlapResult result = model.CheckForTargetObject(rayOriginTransform);

        model.DrawCheckerRay(rayOriginTransform);

        // Ensures that overlaps events only get fired when overlap occurs and not while it is happening.
        if (result.IsOverlapping && !model.IsOverlapping)
        {
            OnBeginOverlapInteractable?.Invoke(result.OverlapObject);
            Debug.Log("overlapped!");
        }
        else if (!result.IsOverlapping && model.IsOverlapping)
        {
            OnExitOverlapInteractable?.Invoke();
            Debug.Log("exited");
        }
    }

    private void FillTargetObject(IInteractable _targetObject)
    {
        model.IsOverlapping = true;
        model.UpdateTargetObject(_targetObject);
    }

    private void ClearTargetObject()
    {
        model.IsOverlapping = false;
        model.UpdateTargetObject(null);
    }

    private void Interact()
    {
        Debug.Log("interacting");
        model.Interact();
    }

    private void PauseMovement()
    {
        model.SetStateMovement(false);
    }

    private void ResumeMovement()
    {
        model.SetStateMovement(true);
    }
}
