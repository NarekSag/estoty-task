using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public void Initialize(PlayerScore score)
    {
        score.OnScoreChanged += UpdateText;
        UpdateText(score.Current);
    }

    private void UpdateText(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void UpdateText(string value)
    {
        _scoreText.text = value;
    }
}
