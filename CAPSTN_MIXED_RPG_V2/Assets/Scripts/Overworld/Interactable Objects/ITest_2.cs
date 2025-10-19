using System;
using UnityEngine;

[Serializable]
public class ITest_2 : InteractableObject
{
    public override void Interact()
    {
        base.Interact();

        Debug.Log("Im test 2. wewe");
    }
}
