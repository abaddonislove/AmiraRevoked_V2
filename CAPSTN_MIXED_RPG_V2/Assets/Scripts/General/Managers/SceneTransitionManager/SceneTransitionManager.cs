using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class SceneTransitionManager : PersistentSingleton<SceneTransitionManager>
{
    [SerializeField]
    private string overworldSceneName;
    [SerializeField]
    private string combatSceneName;

    #region Events
    public event Action OnLoadCombatScene;
    public event Action OnLoadOverworldScene;
    #endregion

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadOverworldScene()
    {
        SceneManager.LoadScene(overworldSceneName);
    }

    public void LoadCombatScene()
    {
        OnLoadCombatScene?.Invoke();
        SceneManager.LoadScene(combatSceneName);
    }

    public void LoadCombatSceneTest()
    {
        SceneManager.LoadScene(combatSceneName);
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name == overworldSceneName)
        {
            FireOverwoldSceneLoaded();
        }
    }

    private void FireOverwoldSceneLoaded()
    {
        OnLoadOverworldScene?.Invoke();
    }
}
