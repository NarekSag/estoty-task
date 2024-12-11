using System;

public interface IHealth
{
    event Action<float> OnHealthChanged;
    float Current { get; }
    float Max { get; }
}