using System;
using UnityEngine;

public class EntityHealth : IHealth, IDamage
{
    public event Action OnHealthChanged;
    public event Action OnDeath;

    private float _current;
    private float _max;

    public float Current => _current;

    public float Max => _max;

    public EntityHealth(float maxHealth)
    {
        _max = maxHealth;
        _current = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_current <= 0) return;

        _current = Mathf.Clamp(_current - damage, 0, _max);

        OnHealthChanged?.Invoke();

        if (_current <= 0)
        {
            OnDeath?.Invoke();
            OnDeath = null;
        }
    }
}