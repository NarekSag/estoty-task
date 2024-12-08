using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ProjectileSpawner
{
    private readonly ProjectileFactory _factory;

    public ProjectileSpawner(ProjectileFactory factory)
    {
        _factory = factory;
        SetupSpawnerParent();
    }

    private Transform _parent;

    public async UniTask StartSpawning(EntityType type, ConfigContainer.ProjectileConfig config, Transform spawnLocation)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(config.FireInterval));

            SpawnProjectile(type, config, spawnLocation);
        }
    }

    private void SpawnProjectile(EntityType type,  ConfigContainer.ProjectileConfig config, Transform spawnLocation)
    {
        Projectile projectile = _factory.CreateProjectile(type, config);

        projectile.transform.SetParent(_parent);
        projectile.transform.position = spawnLocation.position;
    }

    // To keep the inspector organized, we will create a ProjectileSpawner object 
    // so that all projectiles are stored within this parent object.
    private void SetupSpawnerParent()
    {
        GameObject parentObject = new GameObject("ProjectileSpawner");
        _parent = parentObject.transform;
    }
}
