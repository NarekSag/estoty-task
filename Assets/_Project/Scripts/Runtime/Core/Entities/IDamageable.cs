using System;

public interface IDamageable
{
    void Kill();
    void TakeDamage(float damage);
    event Action OnDeath;
}