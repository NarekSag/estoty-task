using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : EntityController, IPowerUpReceiver
{
    [SerializeField] private Transform _projectileSpawnLocation;

    private PlayerInput _input;
    private PlayerScore _score;

    private ProjectileSpawner _projectileSpawner;

    public PlayerScore Score => _score;

    public void Initialize(ConfigContainer.PlayerConfig config, IInput input, ProjectileFactory projectileFactory)
    {
        InitializePlayerInput(input, Body);
        InitializePlayerScore();

        InitializeProjectileSpawner(projectileFactory, config.ProjectileConfig);

        base.Initialize(config.Health);
    }

    protected override void FixedUpdate()
    {
        _input.HandleInput();
    }

    private void InitializePlayerInput(IInput input, Rigidbody body)
    {
        _input = new PlayerInput(input, body);
    }

    private void InitializePlayerScore()
    {
        _score = new PlayerScore();
    }

    private void InitializeProjectileSpawner(ProjectileFactory projectileFactory, ConfigContainer.ProjectileConfig projectileConfig)
    {
        _projectileSpawner = new ProjectileSpawner(projectileFactory);
        _projectileSpawner.StartSpawning(EntityType.Player, projectileConfig, _projectileSpawnLocation).Forget();
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
        _projectileSpawner.Stop();
    }

    public void AddPowerUp(PowerUp.PowerUpType type)
    {
        switch(type)
        {
            case PowerUp.PowerUpType.FIRE_RATE:
                _projectileSpawner.DecreaseFireInterval(0.1f);
                break;
            case PowerUp.PowerUpType.DAMAGE:
                _projectileSpawner.IncreaseDamage(1);
                break;
            case PowerUp.PowerUpType.PROJECTILE_SPEED:
                _projectileSpawner.IncreaseSpeed(0.2f);
                break;
            case PowerUp.PowerUpType.HEALTH:
                Health.AddHealth();
                break;
        }
    }
}

public interface IPowerUpReceiver
{
    void AddPowerUp(PowerUp.PowerUpType type);
}