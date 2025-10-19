using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private GameObject playerModel;

    public GameObject GetPlayerModel()
    {
        return playerModel;
    }
}
