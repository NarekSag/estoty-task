using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : EntityController
{
    [SerializeField] private PowerUp _prefabPowerUp;

    protected float _speed;
    private float _powerUpSpawnChance = 0.1f;

    private ProjectileSpawner _projectileSpawner;

    public void Initialize(ConfigContainer.EnemyConfig config, ProjectileFactory projectileFactory)
    {
        _powerUpSpawnChance = config.PowerUpSpawnChance;
        _speed = config.Speed;

        base.Initialize(config.Health, projectileFactory, config.ProjectileConfig);
    }

    protected override void FixedUpdate()
    {
        var p = _body.position;
        p += Vector3.down * (_speed * Time.deltaTime);
        _body.MovePosition(p);
    }

    protected override void InitializeProjectileSpawner(ProjectileFactory projectileFactory, ConfigContainer.ProjectileConfig projectileConfig)
    {
        _projectileSpawner = new ProjectileSpawner(projectileFactory, transform);
        _projectileSpawner.StartSpawning(EntityType.Enemy, projectileConfig, transform).Forget();
    }

    protected override void HandleDeath()
    {
        _projectileSpawner.Stop();

        if (UnityEngine.Random.value < _powerUpSpawnChance)
        {
            var powerup = Instantiate(_prefabPowerUp);
            var types = Enum.GetValues(typeof(PowerUp.PowerUpType)).Cast<PowerUp.PowerUpType>().ToList();
            powerup.SetType(types[UnityEngine.Random.Range(0, types.Count)]);
        }

        base.HandleDeath();
    }
}
