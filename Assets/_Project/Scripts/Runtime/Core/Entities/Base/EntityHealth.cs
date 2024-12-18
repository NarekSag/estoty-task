using System;
using UnityEngine;

public class EntityHealth : MonoBehaviour, IHealth, IDamageable
{
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private float _current;
    private float _max;

    public float Current => _current;

    public float Max => _max;

    public bool IsAlive => _current > 0;

    public void Initialize(float maxHealth)
    {
        _max = maxHealth;
        _current = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_current <= 0) return;

        _current = Mathf.Clamp(_current - damage, 0, _max);

        OnHealthChanged?.Invoke(_current);

        if (_current <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Kill()
    {
        _current = 0;
        OnDeath?.Invoke();
    }

    public void AddHealth()
    {
        _current = Mathf.Clamp(_current + 1, 0, _max);

        OnHealthChanged?.Invoke(_current);
    }
}