using System.Collections.Generic;
using UnityEngine;

public class SkillData : ScriptableObject
{
    public string SkillName;

    public virtual void ExecuteSkill(UnitData _caster, List<UnitData> _targets)
    {

    }
}
