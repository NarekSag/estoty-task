using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class EnemyProjectileHandler
{
    private CancellationTokenSource _projectileSpawnerCts;

    public EnemyProjectileHandler()
    {
        _projectileSpawnerCts = new CancellationTokenSource();
    }

    public void Initialize(
        ConfigContainer.ProjectileConfig projectileConfig,
        ProjectileSpawner sharedProjectileSpawner,
        Transform enemyTransform)
    {
        sharedProjectileSpawner.StartSpawning(
            EntityType.Enemy,
            projectileConfig,
            enemyTransform,
            _projectileSpawnerCts.Token
        ).Forget();
    }

    public void Reset()
    {
        _projectileSpawnerCts?.Cancel();
        _projectileSpawnerCts?.Dispose();
        _projectileSpawnerCts = null;
    }
}