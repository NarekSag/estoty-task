using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : BaseView
{
    [SerializeField] private Button retryButton;
    [SerializeField] private ScoreView scoreView;

    public Action OnRetry; 

    private void Start()
    {
        retryButton.onClick.AddListener(Retry);
    }

    public void Show(int score)
    {
        scoreView.UpdateText($"SCORE: {score}");

        base.Show();
    }

    private void Retry()
    {
        OnRetry?.Invoke();
    }
}
