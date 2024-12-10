using TMPro;
using UnityEngine;

public class GameplayView : BaseView
{
    [SerializeField] private ScoreView _score;
    [SerializeField] private HealthView _health;

    public void Initialize(PlayerController player)
    {
        _health.Initialize(player);
        _score.UpdateText(0);
    }
}
