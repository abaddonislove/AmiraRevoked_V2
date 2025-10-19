using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OverworldObjectTracker : PersistentSingleton<OverworldObjectTracker>
{
    [Serializable]
    public struct ObjectTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    public ObjectTransform PlayerAvatarTranform;
    public List<ObjectTransform> EnemyTransforms;
    public List<ObjectTransform> OverworldObjectTransforms;

    public event Action OnTrackComplete;

    void Start()
    {
        InitializeSceneTransitionEventListeners();
    }

    private void InitializeSceneTransitionEventListeners()
    {
        SceneTransitionManager.Instance.OnLoadCombatScene += TrackTransforms;
        SceneTransitionManager.Instance.OnLoadOverworldScene += ApplyTransforms;
    }

    public void TrackTransforms()
    {
        // ensuring that the variables are empty.
        ClearTransforms();

        PlayerAvatarTranform = SaveTransform(OverworldEntityManager.Instance.PlayerAvatar);

        foreach (GameObject enemyObject in OverworldEntityManager.Instance.EnemyGameObjects)
        {
            EnemyTransforms.Add(SaveTransform(enemyObject));
        }

        foreach (GameObject overworldObject in OverworldEntityManager.Instance.OverworldGameObjects)
        {
            OverworldObjectTransforms.Add(SaveTransform(overworldObject));
        } 
    }

    public void ClearTransforms()
    {
        PlayerAvatarTranform = new ObjectTransform();
        EnemyTransforms.Clear();
        OverworldObjectTransforms.Clear();
    }

    public void ApplyTransforms()
    {
        OverworldEntityManager oem = OverworldEntityManager.Instance;

        SetTransform(oem.PlayerAvatar, PlayerAvatarTranform);

        for (int i = 0; i < oem.EnemyGameObjects.Count; i++)
        {
            SetTransform(oem.EnemyGameObjects[i], EnemyTransforms[i]);
        }

        for (int i = 0; i < oem.OverworldGameObjects.Count; i++)
        {
            SetTransform(oem.OverworldGameObjects[i], OverworldObjectTransforms[i]);
        }
    }

    private void SetTransform(GameObject _object, ObjectTransform _transform)
    {
        Transform ot = _object.transform;

        ot.position = _transform.position;
        ot.rotation = _transform.rotation;
        ot.localScale = _transform.scale;
    }

    private ObjectTransform SaveTransform(GameObject _object)
    {
        ObjectTransform newTransform;
        Transform ot = _object.transform;

        newTransform.position = ot.position;
        newTransform.rotation = ot.rotation;
        newTransform.scale = ot.localScale;

        return newTransform;
    }
}
