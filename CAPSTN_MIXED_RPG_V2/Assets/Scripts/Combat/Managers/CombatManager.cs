using System.Collections.Generic;
using UnityEngine;

// Handles rounds. 
// Rounds: Unit Setup --> Action Choosing --> Check Turn Order --> Execute Actions --> End/ Repeat Round.
public class CombatManager : Singleton<CombatManager>
{
    private List<UnitData> unitTurnList;

    void Start()
    {
        InitializeTurnList();
    }

    private void InitializeTurnList()
    {
        foreach (UnitData unit in UnitManager.Instance.AllUnits)
        {
            unitTurnList.Add(unit);
        }
    }

    private void UpdateTurnOrder()
    {
        unitTurnList.Sort();
    }
}
