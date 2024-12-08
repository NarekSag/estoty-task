using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : EntityController
{
    [SerializeField] private PowerUp _prefabPowerUp;

    protected float _speed;
    private float _powerUpSpawnChance = 0.1f;
    private CancellationTokenSource _projectileSpawnerCts;

    public void Initialize(ConfigContainer.EnemyConfig config)
    {
        _powerUpSpawnChance = config.PowerUpSpawnChance;
        _speed = config.Speed;

        _projectileSpawnerCts = new CancellationTokenSource();

        base.Initialize(config.Health);
    }

    protected override void FixedUpdate()
    {
        var p = _body.position;
        p += Vector3.down * (_speed * Time.deltaTime);
        _body.MovePosition(p);
    }

    public void InitializeProjectileSpawner(
        ConfigContainer.ProjectileConfig projectileConfig,
        ProjectileSpawner sharedProjectileSpawner)
    {
        sharedProjectileSpawner.StartSpawning(
            EntityType.Enemy,
            projectileConfig,
            transform,
            _projectileSpawnerCts.Token
        ).Forget();
    }

    protected override void HandleDeath()
    {
        _projectileSpawnerCts?.Cancel();
        _projectileSpawnerCts?.Dispose();
        _projectileSpawnerCts = null;

        if (UnityEngine.Random.value < _powerUpSpawnChance)
        {
            var powerup = Instantiate(_prefabPowerUp);
            var types = Enum.GetValues(typeof(PowerUp.PowerUpType)).Cast<PowerUp.PowerUpType>().ToList();
            powerup.SetType(types[UnityEngine.Random.Range(0, types.Count)]);
        }

        base.HandleDeath();
    }
}