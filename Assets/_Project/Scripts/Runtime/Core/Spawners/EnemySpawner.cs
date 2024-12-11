using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class EnemySpawner
{
    private readonly EnemyFactory _enemyFactory;
    private readonly ProjectileSpawner _projectileSpawner;
    private Transform _parent;

    public event Action<EnemyController> OnSpawn;

    private CancellationTokenSource _cancellationTokenSource;
    private int _spawnedEnemiesCount;
    private float _currentSpawnInterval;

    public EnemySpawner(EnemyFactory enemyFactory, ProjectileFactory projectileFactory)
    {
        _enemyFactory = enemyFactory;
        _projectileSpawner = new ProjectileSpawner(projectileFactory);
    }

    public async UniTask StartSpawning(ConfigContainer.EnemyConfig config)
    {
        Stop();

        _cancellationTokenSource = new CancellationTokenSource();
        ConfigContainer.SpawnerConfig spawnerConfig = config.SpawnerConfig;
        
        _spawnedEnemiesCount = 0;
        _currentSpawnInterval = spawnerConfig.SpawnInterval;

        try
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_currentSpawnInterval), cancellationToken: _cancellationTokenSource.Token);
                SpawnEnemy(config);

                _spawnedEnemiesCount++;

                if (_spawnedEnemiesCount % 10 == 0)
                {
                    _currentSpawnInterval = Mathf.Max(spawnerConfig.MinSpawnInterval, _currentSpawnInterval - spawnerConfig.IntervalDecreaseAmount);
                    Debug.Log($"Spawn interval decreased to: {_currentSpawnInterval}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Spawning cancelled.");
        }
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
    }

    private void SpawnEnemy(ConfigContainer.EnemyConfig config)
    {
        EnemyController enemy = _enemyFactory.CreateEnemy(config);
        if (enemy == null) return;

        SetParent(enemy);
        SetRandomPosition(enemy, config.HorizontalSpawnRange);
        SetProjectileSpawner(enemy, config.ProjectileConfig);

        OnSpawn?.Invoke(enemy);
    }

    private void SetProjectileSpawner(EnemyController enemy, ConfigContainer.ProjectileConfig projectileConfig)
    {
        if (UnityEngine.Random.value < 0.4f)
        {
            enemy.ProjectileHandler.Initialize(projectileConfig, _projectileSpawner, enemy.transform);
        }
    }

    private void SetParent(EnemyController enemy)
    {
        if (_parent == null) SetupSpawnerParent();

        enemy.transform.SetParent(_parent);
        enemy.transform.localPosition = Vector3.zero;
    }

    private void SetRandomPosition(EnemyController enemy, float horizontalSpawnRange)
    {
        enemy.transform.position = new Vector3(GetRandomHorizontalPosition(horizontalSpawnRange), enemy.transform.position.y, 0);
    }

    private float GetRandomHorizontalPosition(float horizontalSpawnRange)
    {
        return UnityEngine.Random.Range(-horizontalSpawnRange, horizontalSpawnRange);
    }

    private void SetupSpawnerParent()
    {
        GameObject parentObject = new GameObject("EnemySpawner");
        parentObject.transform.position = new Vector3(0, 14, 0);
        _parent = parentObject.transform;
    }
}
