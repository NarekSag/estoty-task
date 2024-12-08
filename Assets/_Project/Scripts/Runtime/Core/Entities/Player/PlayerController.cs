using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : EntityController
{
    [SerializeField] private Transform _projectileSpawnLocation;

    private IInput _input;
    private float _movementRangeMin;
    private float _movementRangeMax;

    public void Initialize(ConfigContainer.PlayerConfig config, IInput input, ProjectileFactory projectileFactory)
    {
        _input = input;
        _movementRangeMin = config.MovementRangeMin;
        _movementRangeMax = config.MovementRangeMax;

        base.Initialize(config.Health, projectileFactory, config.ProjectileConfig);
    }

    protected override void FixedUpdate()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            HandleInput();
    }

    private void HandleInput()
    {
        float xPos = _input.Direction.x / Screen.width;
        _body.MovePosition(new Vector3(Mathf.Lerp(_movementRangeMin, _movementRangeMax, xPos), 0.0f, 0.0f));
    }

    protected override void InitializeProjectileSpawner(ProjectileFactory projectileFactory, ConfigContainer.ProjectileConfig projectileConfig)
    {
        ProjectileSpawner projectileSpawner = new ProjectileSpawner(projectileFactory);
        projectileSpawner.StartSpawning(EntityType.Player, projectileConfig, _projectileSpawnLocation).Forget();
    }

    public void AddPowerUp(PowerUp.PowerUpType type)
    {
        if(type.Equals(PowerUp.PowerUpType.FIRE_RATE))
        {
            //TODO: UPDATE FireIntervalValue in ProjectileSpawner
        }
    }
}