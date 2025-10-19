using System;
using System.Collections.Generic;
using UnityEngine;

// Variables: Health, Speed, Attack, Defense, Queued Action
public class UnitData : MonoBehaviour, IComparable<UnitData>
{
    public enum Faction
    {
        Ally,
        Enemy
    }

    public float MaxHealth;
    public float CurrentHealth;
    public float Speed;
    public float Attack;
    public float Defense;
    public bool IsAlive = true;
    public Faction V_Faction;
    public List<SkillData> Skills;
    public SkillData SelectedSkill;

    public event Action<UnitData> OnDie;
    public event Action OnActionFinishedPerforming;

    #region Interface

    public int CompareTo(UnitData _other)
    {
        if (_other == null) return 1;
        return Speed.CompareTo(_other.Speed);
    }

    #endregion

    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float _amount)
    {
        CurrentHealth -= _amount;
    }

    public void RegainHealth(float _amount)
    {
        CurrentHealth += _amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    public void CheckIfAlive()
    {
        if (CurrentHealth > 0) return;

        IsAlive = false;
        OnDie?.Invoke(this);
    }

    public virtual void ChooseSkill()
    {
        int index = UnityEngine.Random.Range(0, Skills.Count);
        Debug.Log(index);
        SelectedSkill = Skills[index];
    }

    public virtual void PerformAction(List<UnitData> _targets)
    {
        SelectedSkill.ExecuteSkill(this, _targets);
    }
}
