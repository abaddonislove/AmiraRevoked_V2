using UnityEngine;

public class SceneLoadingButtons : MonoBehaviour
{
    public void LoadOverworld()
    {
        SceneTransitionManager.Instance.LoadOverworldScene();
    }

    public void LoadCombat()
    {
        SceneTransitionManager.Instance.LoadCombatScene();
    }
}
