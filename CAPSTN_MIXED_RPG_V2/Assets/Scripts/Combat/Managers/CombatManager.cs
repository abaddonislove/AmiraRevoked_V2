using System.Collections.Generic;
using UnityEngine;

// Handles rounds. 
// Rounds: Unit Setup --> Action Choosing --> Check Turn Order --> Execute Actions --> End/ Repeat Round.
// Action: Play Animation --> Apply Effect --> Move to next action.
public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    private List<UnitData> unitTurnList;

    void Start()
    {
        // Add units to turn order.
        InitializeTurnList();

        // Sort units in order of speed.
        UpdateTurnOrder();
    }

    public void PlayRound()
    {
        foreach (UnitData unit in unitTurnList)
        {
            unit.ChooseSkill();
        }

        foreach (UnitData unit in unitTurnList)
        {
            List<UnitData> targets = new List<UnitData>();
            targets.Add(unitTurnList[0]);
            unit.PerformAction(targets);
        }
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
