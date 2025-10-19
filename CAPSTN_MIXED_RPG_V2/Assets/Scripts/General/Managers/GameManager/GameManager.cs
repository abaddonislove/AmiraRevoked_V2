using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool IsGamePaused;

    public event Action OnGamePause;
    public event Action OnGameResume;

    public void TogglePause()
    {
        IsGamePaused = !IsGamePaused;

        if (IsGamePaused)
        {
            OnGamePause?.Invoke();
        }
        else
        {
            OnGameResume?.Invoke();
        }
    }
}
