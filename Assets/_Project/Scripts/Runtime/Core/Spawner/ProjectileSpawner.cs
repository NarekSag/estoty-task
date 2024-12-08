using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using System.Threading;

public class ProjectileSpawner
{
    private readonly ProjectileFactory _factory;
    private Transform _parent;
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

    public async UniTask StartSpawning(EntityType type, ConfigContainer.ProjectileConfig config, Transform spawnLocation)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        try
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(config.FireInterval),
                    cancellationToken: _cancellationTokenSource.Token
                );

                if (_cancellationTokenSource.IsCancellationRequested)
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
        projectile.transform.SetParent(_parent);
        projectile.transform.position = spawnLocation.position;
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
    }

    // To keep the inspector organized, we will create a ProjectileSpawner object 
    // so that all projectiles are stored within this parent object.
    private void SetupSpawnerParent(Transform parent = null)
    {
        _parent = parent ?? new GameObject("ProjectileSpawner").transform;
    }
}