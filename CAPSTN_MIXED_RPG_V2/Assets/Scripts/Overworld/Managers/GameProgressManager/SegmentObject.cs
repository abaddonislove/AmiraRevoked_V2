using System;
using UnityEngine;

// This class is to be inherrited by objects that change/ update when a segment is completed/ made available.
public class SegmentObject : MonoBehaviour
{
    protected enum RequirementType
    {
        Completion,
        Unlock
    }

    [SerializeField]
    protected RequirementType requirement;
    [SerializeField]
    protected string segmentId;

    protected void Start()
    {
        InitializeListeners();
        Debug.Log("adding listeners");
    }

    private void InitializeListeners()
    {
        GameProgressManager.Instance.OnSegmentCompleted += OnSegmentCompleted;
        GameProgressManager.Instance.OnSegmentUnlocked += OnSegmentUnlocked;
    }

    protected virtual void OnSegmentCompleted(string _completedSegment)
    {
        Debug.Log("completed and checking");
        if (requirement == RequirementType.Completion)
        {
            if (CheckObjectRequirements(_completedSegment))
            {
                PerformAction();
            }
        }
    }

    protected virtual void OnSegmentUnlocked(string _unlockedSegment)
    {
        if (requirement == RequirementType.Unlock)
        {
            if (CheckObjectRequirements(_unlockedSegment))
            {
                PerformAction();
            }
        }
    }

    protected bool CheckObjectRequirements(string _Id)
    {
        if (segmentId == _Id) return true;

        return false;
    }
    
    protected virtual void PerformAction()
    {
        
    }
}
