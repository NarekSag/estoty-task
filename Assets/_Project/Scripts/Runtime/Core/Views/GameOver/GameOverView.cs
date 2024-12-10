using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : BaseView
{
    [SerializeField] private Button retryButton;

    public Action OnRetry; 

    private void Start()
    {
        retryButton.onClick.AddListener(Retry);
    }

    private void Retry()
    {
        OnRetry?.Invoke();
    }
}
