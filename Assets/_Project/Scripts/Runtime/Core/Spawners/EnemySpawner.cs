using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class EnemySpawner
{
    private readonly EnemyFactory _enemyFactory;
    private readonly ProjectileSpawner _projectileSpawner;

    private Transform _parent;

    public EnemySpawner(EnemyFactory enemyFactory, ProjectileFactory projectileFactory)
    {
        _enemyFactory = enemyFactory;
        _projectileSpawner = new ProjectileSpawner(projectileFactory);
    }

    private CancellationTokenSource _cancellationTokenSource;

    public async UniTask StartSpawning(ConfigContainer.EnemyConfig config)
    {
        Stop();

        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            while (true)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(config.SpawnInterval), cancellationToken: _cancellationTokenSource.Token);
                SpawnEnemy(config);
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

    // To keep the inspector organized, we will create a EnemySpawner object 
    // so that all enemies are stored within this parent object.
    private void SetupSpawnerParent()
    {
        GameObject parentObject = new GameObject("EnemySpawner");
        parentObject.transform.position = new Vector3(0, 14, 0);
        _parent = parentObject.transform;
    }
}
