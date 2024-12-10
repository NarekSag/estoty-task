using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;

public class CoreController : ILoadUnit<ConfigContainer>
{
    private readonly PlayerFactory _playerFactory;
    private readonly EnemyFactory _enemyFactory;
    private readonly ProjectileFactory _projectileFactory;

    private readonly CoreViewController _viewController;

    private EnemySpawner _enemySpawner;

    public PlayerController Player { get; private set; }

    public CoreController(PlayerFactory playerFactory,
        EnemyFactory enemyFactory,
        ProjectileFactory projectileFactory,
        CoreViewController viewController)
    {
        _playerFactory = playerFactory;
        _enemyFactory = enemyFactory;
        _projectileFactory = projectileFactory;

        _viewController = viewController;
    }

    public UniTask Load(ConfigContainer config)
    {
        InitializePlayer(config.Core.PlayerConfig);
        InitializeEnemySpawner(config.Core.EnemyConfig);
        InitializeViewController(config.Core.PlayerConfig.Health);

        return UniTask.CompletedTask;
    }

    private void InitializePlayer(ConfigContainer.PlayerConfig playerConfig)
    {
        Player = _playerFactory.CreatePlayer(playerConfig);
        Player.Health.OnDeath += HandlePlayerDeath;
    }

    private void InitializeEnemySpawner(ConfigContainer.EnemyConfig enemyConfig)
    {
        _enemySpawner = new EnemySpawner(_enemyFactory, _projectileFactory);
        _enemySpawner.StartSpawning(enemyConfig).Forget();
    }

    private void InitializeViewController(int health)
    {
        _viewController.GameplayView.Show();
        _viewController.GameOverView.Hide();

        _viewController.GameplayView.Initialize(health);

        _viewController.GameOverView.OnRetry += RestartCore;
    }

    private void HandlePlayerDeath()
    {
        _viewController.GameplayView.Hide();
        _viewController.GameOverView.Show();
    }

    private void RestartCore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(RuntimeConstants.Scenes.Core);
    }
}
