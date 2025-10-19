using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicAttack", menuName = "Skills/BasicAttack")]
public class BasicAttack : SkillData
{
    public float DamageFactor;

    public override void ExecuteSkill(UnitData _caster, List<UnitData> _targets)
    {
        foreach (UnitData target in _targets)
        {
            target.TakeDamage(_caster.Attack * DamageFactor);
        }
    }
}
