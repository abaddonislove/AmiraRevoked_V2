using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public virtual void Interact()
    {
        Debug.Log("Im interatable!");
    }

    public virtual void DisplayIcon()
    {

    }

    public virtual void HideIcon()
    {
        
    }
}
