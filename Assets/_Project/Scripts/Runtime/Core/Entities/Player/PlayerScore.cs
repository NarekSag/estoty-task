using System;

public class PlayerScore : IScore
{
    private int _currentScore;

    public event Action<int> OnScoreChanged;

    public int Current
    {
        get => _currentScore;
        private set
        {
            if (_currentScore != value)
            {
                _currentScore = value;
                OnScoreChanged?.Invoke(_currentScore);
            }
        }
    }

    public PlayerScore()
    {
        _currentScore = 0;
    }

    public void Add(int points)
    {
        Current += points;
    }

    public void Reset()
    {
        Current = 0;
    }
}