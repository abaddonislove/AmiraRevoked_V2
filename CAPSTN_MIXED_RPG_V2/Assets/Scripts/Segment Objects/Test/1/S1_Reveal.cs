using Unity.VisualScripting;
using UnityEngine;

public class S1_Reveal : SegmentObject
{
    [SerializeField]
    private MeshRenderer meshRenderer;
    
    protected override void PerformAction()
    {
        base.PerformAction();

        meshRenderer.enabled = true;
    }
}
