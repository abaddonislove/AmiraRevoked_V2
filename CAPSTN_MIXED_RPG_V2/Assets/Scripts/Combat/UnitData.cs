using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

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

    public event Action<UnitData> OnDie;

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
}
