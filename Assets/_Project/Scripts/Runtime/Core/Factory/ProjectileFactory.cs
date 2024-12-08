using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;
using System;

public class ProjectileFactory
{
    private readonly ProjectileAssets _projectileAssets;

    private class ProjectileAssets
    {
        public Projectile PlayerProjectile { get; }
        public Projectile EnemyProjectile { get; }

        public ProjectileAssets(string playerProjectilePath, string enemyProjectilePath)
        {
            PlayerProjectile = LoadProjectile(playerProjectilePath, "Player");
            EnemyProjectile = LoadProjectile(enemyProjectilePath, "Enemy");
        }

        private Projectile LoadProjectile(string path, string typeName)
        {
            var projectile = Resources.Load<Projectile>(path);
            if (projectile == null)
            {
                throw new InvalidOperationException(
                    $"Failed to load {typeName} projectile from path: {path}");
            }
            return projectile;
        }
    }

    public ProjectileFactory()
    {
        _projectileAssets = new ProjectileAssets(
            RuntimeConstants.Resources.Projectiles.Player,
            RuntimeConstants.Resources.Projectiles.Enemy
        );
    }

    public Projectile CreateProjectile(EntityType type, ConfigContainer.ProjectileConfig config)
    {
        Projectile projectileObject = type switch
        {
            EntityType.Player => UnityEngine.Object.Instantiate(_projectileAssets.PlayerProjectile),
            EntityType.Enemy => UnityEngine.Object.Instantiate(_projectileAssets.EnemyProjectile),
            _ => throw new ArgumentException($"Unsupported projectile type: {type}")
        };

        projectileObject.Initialize(config);
        return projectileObject;
    }
}