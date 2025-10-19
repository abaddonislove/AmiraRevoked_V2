using System.Collections.Generic;
using UnityEngine;

public class OverworldEntityManager : Singleton<OverworldEntityManager>
{
    public GameObject PlayerAvatar;
    public List<GameObject> EnemyGameObjects;
    public List<GameObject> OverworldGameObjects;
}
