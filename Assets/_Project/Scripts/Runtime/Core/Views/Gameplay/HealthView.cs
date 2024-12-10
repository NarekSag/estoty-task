using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private GameObject _healthPrefab;
    [SerializeField] private int _maxHealth = 10; // Maximum number of hearts

    private List<GameObject> _hearts = new List<GameObject>();

    public void Initialize(PlayerController player)
    {
        ClearHearts();

        player.Health.OnHealthChanged += UpdateHealth;
        UpdateHealth(player.Health.Current);
    }

    public void UpdateHealth(float currentHealth)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                _hearts[i].SetActive(true);
            }
            else
            {
                _hearts[i].SetActive(false);
            }
        }

        if (currentHealth > _hearts.Count)
        {
            AddHearts(currentHealth);
        }
    }

    private void AddHearts(float count)
    {
        for (int i = 0; i < count; i++)
        {
            var heart = Instantiate(_healthPrefab, transform);
            heart.SetActive(true);
            _hearts.Add(heart);
        }
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