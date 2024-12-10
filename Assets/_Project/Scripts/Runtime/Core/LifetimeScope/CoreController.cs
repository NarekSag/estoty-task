using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using System.Collections.Generic;
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
        InitializeViewController();

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
        _enemySpawner.OnSpawn += HandleEnemySpawn;
    }

    private void InitializeViewController()
    {
        _viewController.GameplayView.Show();
        _viewController.GameOverView.Hide();

        _viewController.GameplayView.Initialize(Player);

        _viewController.GameOverView.OnRetry += RestartCore;
    }

    private void HandlePlayerDeath()
    {
        _viewController.GameplayView.Hide();
        _viewController.GameOverView.Show(Player.Score.Current);

        _enemySpawner.Stop();

        Time.timeScale = 0f;
    }

    private void HandleEnemySpawn(EnemyController enemy)
    {
        enemy.Health.OnDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath()
    {
        Player.Score.Add(1);
    }

    private void RestartCore()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(RuntimeConstants.Scenes.Core);
    }
}
