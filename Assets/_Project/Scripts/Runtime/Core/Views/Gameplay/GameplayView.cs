using TMPro;
using UnityEngine;

public class GameplayView : BaseView
{
    [SerializeField] private ScoreView _score;
    [SerializeField] private HealthView _health;

    public void Initialize(int health)
    {
        _health.Initialize(health);
        _score.UpdateText(0);
    }
}
