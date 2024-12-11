using System;

public interface IScore
{
    int Current { get; }
    void Add(int points);
    void Reset();
    event Action<int> OnScoreChanged;
}