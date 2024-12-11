using UnityEngine;

public class CoreViewController : MonoBehaviour
{
    [SerializeField] private GameplayView gameplayView;
    [SerializeField] private GameOverView gameOverView;

    public GameplayView GameplayView => gameplayView;
    public GameOverView GameOverView => gameOverView;
}
