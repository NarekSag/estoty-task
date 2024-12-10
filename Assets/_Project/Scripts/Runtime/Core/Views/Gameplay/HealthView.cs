using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private GameObject _healthPrefab;

    private List<GameObject> _hearts = new();

    public void Initialize(int health)
    {
        ClearHearts();

        for (int i = 0; i < health; i++)
        {
            AddHeart();
        }
    }

    public void UpdateHealth(int value)
    {
        int activeHearts = GetActiveHeartCount();
        int targetHealth = Mathf.Clamp(activeHearts + value, 0, _hearts.Count);

        if (value < 0)
        {
            for (int i = activeHearts - 1; i >= targetHealth; i--)
            {
                _hearts[i].SetActive(false);
            }
        }
        else if (value > 0)
        {
            for (int i = activeHearts; i < targetHealth; i++)
            {
                _hearts[i].SetActive(true);
            }
        }
    }

    //TODO: Add heart if a player gets a power up
    public void AddHeart()
    {
        GameObject heart = Instantiate(_healthPrefab, transform);
        heart.SetActive(true);
        _hearts.Add(heart);
    }

    private int GetActiveHeartCount()
    {
        int count = 0;
        foreach (var heart in _hearts)
        {
            if (heart.activeSelf) count++;
        }
        return count;
    }

    private void ClearHearts()
    {
        foreach (var heart in _hearts)
        {
            Destroy(heart);
        }
        _hearts.Clear();
    }
}