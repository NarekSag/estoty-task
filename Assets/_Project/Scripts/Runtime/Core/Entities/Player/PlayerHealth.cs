using System;
using UnityEngine;

public class PlayerHealth : IHealth, IDamage
{
    public event Action OnHealthChanged;
    public event Action OnDeath;

    private float _current;
    private float _max;

    public float Current => _current;

    public float Max => _max;

    public PlayerHealth(float maxHealth)
    {
        _max = maxHealth;
        _current = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_current <= 0) return;

        _current = Mathf.Clamp(damage, 0, _max);

        OnHealthChanged?.Invoke();

        if(_current <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}

internal interface IDamage
{
    event Action OnDeath;
    void TakeDamage(float damage);
}

public interface IHealth
{
    event Action OnHealthChanged;
    float Current { get; }
    float Max { get; }
}