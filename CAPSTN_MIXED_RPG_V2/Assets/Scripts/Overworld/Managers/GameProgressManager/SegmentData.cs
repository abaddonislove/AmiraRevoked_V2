using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Game Segment", menuName = "Game Segement/ Segment Data")]
public class SegmentData : MonoBehaviour //ScriptableObject
{
    public string SegmentID;
    public bool IsMainSegment;
    public bool IsCompleted;
    public bool IsAvailable;
    public List<string> RequiredSegments; 
}
