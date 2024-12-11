using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using System.Threading;

public class ProjectileSpawner
{
    private readonly ProjectileFactory _factory;
    private Transform _parent;

    private int _currentDamage;
    private float _currentFireInterval;
    private float _currentSpeed;

    private CancellationTokenSource _cancellationTokenSource;

    public ProjectileSpawner(ProjectileFactory factory)
    {
        _factory = factory;
        SetupSpawnerParent();
    }

    public ProjectileSpawner(ProjectileFactory factory, Transform parent)
    {
        _factory = factory;
        SetupSpawnerParent(parent);
    }

    public async UniTask StartSpawning(EntityType type, ConfigContainer.ProjectileConfig config, Transform spawnLocation, CancellationToken externalCancellationToken = default)
    {
        Stop();
        _cancellationTokenSource = new CancellationTokenSource();
        _currentFireInterval = config.FireInterval;
        _currentDamage = config.Damage;
        _currentSpeed = config.Speed;

        var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(
            _cancellationTokenSource.Token,
            externalCancellationToken
        ).Token;

        try
        {
            SpawnProjectile(type, config, spawnLocation);

            while (!combinedToken.IsCancellationRequested)
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(_currentFireInterval),
                    cancellationToken: combinedToken
                );

                if (combinedToken.IsCancellationRequested)
                    break;

                SpawnProjectile(type, config, spawnLocation);
            }
        }
        finally
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private void SpawnProjectile(EntityType type, ConfigContainer.ProjectileConfig config, Transform spawnLocation)
    {
        Projectile projectile = _factory.CreateProjectile(type, config);
        projectile.SetDamage(_currentDamage);
        projectile.SetSpeed(_currentSpeed);
        projectile.transform.SetParent(_parent);
        projectile.transform.position = spawnLocation.position;
    }

    private void SetupSpawnerParent(Transform parent = null)
    {
        _parent = parent ?? new GameObject("ProjectileSpawner").transform;
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = null;
    }

    public void DecreaseFireInterval(float decreaseAmount, float minInterval = 0.1f)
    {
        _currentFireInterval = Mathf.Max(minInterval, _currentFireInterval - decreaseAmount);
    }

    public void IncreaseDamage(int amount)
    {
        _currentDamage += amount;
    }

    public void IncreaseSpeed(float amount)
    {
        _currentSpeed += amount;
    }
}
