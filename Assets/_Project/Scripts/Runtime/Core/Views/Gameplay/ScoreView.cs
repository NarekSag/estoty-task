using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public void UpdateText(int newScore)
    {
        _scoreText.text = newScore.ToString();
    }
}
