using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : Singleton<GameProgressManager>
{
    [SerializeField]
    private List<SegmentData> gameSegments = new List<SegmentData>();
    [SerializeField]
    private Dictionary<string, SegmentData> gameSegmentDictionary = new Dictionary<string, SegmentData>();
    [SerializeField]
    private List<SegmentData> mainSegments;
    [SerializeField]
    private int mainSegmentIndex = 0;

    public event Action<string> OnSegmentCompleted;
    public event Action<string> OnSegmentUnlocked;

    void Start()
    {
        SetupSegmentContainers();
        StartCoroutine(CO_Test());
    }

    public void CompleteSegment(string _segmentId)
    {
        SegmentData targetSegment = null;

        foreach (SegmentData segment in gameSegments)
        {
            if (segment.SegmentID == _segmentId)
            {
                targetSegment = segment;
                break;
            }
        }

        if (targetSegment == null) return;

        targetSegment.IsCompleted = true;
        targetSegment.IsAvailable = false;

        if (targetSegment.IsMainSegment)
        {
            mainSegmentIndex++;
        }

        // Checks and updates unavailable segments for if they're ready to be available.
        UpdateAvailableSegments();

        // Let everyone know segment is completed.
        OnSegmentCompleted?.Invoke(targetSegment.SegmentID);
    }

    public SegmentData GetCurrentMainSegment()
    {
        return mainSegments[mainSegmentIndex];
    }

    public void UpdateAvailableSegments()
    {
        foreach (SegmentData segment in gameSegments)
        {
            if (segment.IsCompleted || segment.IsAvailable) continue;

            bool isReqAccomplished = true;

            foreach (string reqSegments in segment.RequiredSegments)
            {
                if (!gameSegmentDictionary[reqSegments].IsCompleted)
                {
                    isReqAccomplished = false;
                }
            }

            if (isReqAccomplished)
            {
                segment.IsAvailable = true;

                // Let everyone know segment is available.
                OnSegmentUnlocked?.Invoke(segment.SegmentID);
            }
        }
    }

    private void SetupSegmentContainers()
    {
        // Segment dictionary for fast lookups.
        foreach (SegmentData segment in gameSegments)
        {
            gameSegmentDictionary.Add(segment.SegmentID, segment);
        }

        // Main segment list.
        for (int i = 0; i < gameSegments.Count; i++)
        {
            if (gameSegments[i].IsMainSegment)
            {
                mainSegments.Add(gameSegments[i]);
            }
        }
    }
    
    #region Testing
    private IEnumerator CO_Test()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("updating segments");
        CompleteSegment(GetCurrentMainSegment().SegmentID);
    }
    #endregion
}
