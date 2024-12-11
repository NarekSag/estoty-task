using TMPro;
using UnityEngine;

public class GameplayView : BaseView
{
    [SerializeField] private ScoreView _score;
    [SerializeField] private HealthView _health;
    [SerializeField] private PowerUpView _powerUpView;

    public PowerUpView PowerUpView => _powerUpView;

    public void Initialize(PlayerController player)
    {
        _health.Initialize(player.Health);
        _score.Initialize(player.Score);
    }
}
