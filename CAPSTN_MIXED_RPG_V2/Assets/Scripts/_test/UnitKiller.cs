using UnityEngine;

public class UnitKiller : MonoBehaviour
{
    public void KillUnit(UnitData _unit)
    {
        _unit.TakeDamage(_unit.MaxHealth);
        _unit.CheckIfAlive();
    }
}
