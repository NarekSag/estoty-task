using System;

public interface IDamage
{
    event Action OnDeath;
    void TakeDamage(float damage);
}