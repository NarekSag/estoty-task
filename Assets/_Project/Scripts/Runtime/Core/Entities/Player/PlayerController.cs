using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : EntityController
{
    [SerializeField] private Transform _projectileSpawnLocation;

    private PlayerInput _input;
    private ProjectileSpawner _projectileSpawner;

    public void Initialize(ConfigContainer.PlayerConfig config, IInput input, ProjectileFactory projectileFactory)
    {
        InitializePlayerInput(input, _body);

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
        if(type.Equals(PowerUp.PowerUpType.FIRE_RATE))
        {
            //TODO: UPDATE FireIntervalValue in ProjectileSpawner
        }
    }
}