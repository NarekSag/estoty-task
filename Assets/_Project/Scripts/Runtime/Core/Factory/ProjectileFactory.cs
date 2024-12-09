using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;
using System;

public class ProjectileFactory
{
    private readonly Projectile _playerProjectilePrefab;
    private readonly Projectile _enemyProjectilePrefab;
    private readonly ObjectPool<Projectile> _playerProjectilePool;
    private readonly ObjectPool<Projectile> _enemyProjectilePool;

    public ProjectileFactory()
    {
        _playerProjectilePrefab = ResourceLoader.Load<Projectile>(RuntimeConstants.Resources.Projectiles.Player);
        _enemyProjectilePrefab = ResourceLoader.Load<Projectile>(RuntimeConstants.Resources.Projectiles.Enemy);

        _playerProjectilePool = new ObjectPool<Projectile>(_playerProjectilePrefab);
        _enemyProjectilePool = new ObjectPool<Projectile>(_enemyProjectilePrefab);
    }

    public Projectile CreateProjectile(EntityType type, ConfigContainer.ProjectileConfig config)
    {
        Projectile projectileObject = type switch
        {
            EntityType.Player => _playerProjectilePool.Get(),
            EntityType.Enemy => _enemyProjectilePool.Get(),
            _ => throw new ArgumentException($"Unsupported projectile type: {type}")
        };

        projectileObject.Initialize(config, type);
        projectileObject.gameObject.SetActive(true);
        projectileObject.OnDestroy += ReturnProjectile;

        return projectileObject;
    }

    public void ReturnProjectile(Projectile projectile)
    {
        projectile.OnDestroy -= ReturnProjectile;

        switch (projectile.EntityType)
        {
            case EntityType.Player:
                _playerProjectilePool.Return(projectile);
                break;
            case EntityType.Enemy:
                _enemyProjectilePool.Return(projectile);
                break;
        }
    }
}