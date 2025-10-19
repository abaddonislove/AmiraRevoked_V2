using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    public List<UnitData> AllUnits;

    [Header("Allies"), Space(10f)]
    public List<UnitData> AllyUnits;
    public List<UnitData> LivingAllies;
    public List<UnitData> DeadAllies;

    [Header("Enemies"), Space(10f)]
    public List<UnitData> EnemyUnits;
    public List<UnitData> LivingEnemies;
    public List<UnitData> DeadEnemies;

    protected override void Awake()
    {
        base.Awake();
        InitializeListeners();
        InitializeUnitLists();
    }

    private void InitializeListeners()
    {
        foreach (UnitData unit in AllyUnits)
        {
            unit.OnDie += MoveUnitToDeadList;
        }

        foreach (UnitData unit in EnemyUnits)
        {
            unit.OnDie += MoveUnitToDeadList;
        }
    }

    public void InitializeUnitLists()
    {
        foreach (UnitData unit in AllUnits)
        {
            if (unit.V_Faction == UnitData.Faction.Ally)
            {
                AllyUnits.Add(unit);

                if (unit.IsAlive)
                {
                    LivingAllies.Add(unit);
                }
                else
                {
                    DeadAllies.Add(unit);
                }
            }
            else
            {
                EnemyUnits.Add(unit);

                if (unit.IsAlive)
                {
                    LivingEnemies.Add(unit);
                }
                else
                {
                    DeadEnemies.Add(unit);
                }
            }
        }
    }

    public void MoveUnitToDeadList(UnitData _unit)
    {
        if (AllyUnits.Contains(_unit))
        {
            LivingAllies.Remove(_unit);
            DeadAllies.Add(_unit);
        }
        else if (EnemyUnits.Contains(_unit))
        {
            LivingEnemies.Remove(_unit);
            DeadEnemies.Add(_unit);
        }
    }
}
