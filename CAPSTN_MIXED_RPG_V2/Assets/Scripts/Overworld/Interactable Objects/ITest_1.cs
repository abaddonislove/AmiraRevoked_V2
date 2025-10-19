using UnityEngine;

public class ITest_1 : InteractableObject
{
    public override void Interact()
    {
        base.Interact();

        Debug.Log("Im test 1");
    }
}
