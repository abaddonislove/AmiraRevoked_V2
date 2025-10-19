using Unity.VisualScripting;
using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this as T)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this as T;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
