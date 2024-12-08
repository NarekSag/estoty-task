using System;

public interface IHealth
{
    event Action OnHealthChanged;
    float Current { get; }
    float Max { get; }
}