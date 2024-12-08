using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _prefabExplosion;
    [SerializeField] private Transform _projectileSpawnLocation;

    public event Action OnDie;
    public event Action OnHit;

    private float _movementRangeMin;
    private float _movementRangeMax;

    private Rigidbody _body;
    private IInput _input;

    private PlayerHealth _health;

    public PlayerHealth Health => _health;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public void Initialize(ConfigContainer.PlayerConfig config, IInput input, ProjectileFactory projectileFactory)
    {
        _input = input;

        _movementRangeMin = config.MovementRangeMin;
        _movementRangeMax = config.MovementRangeMax;

        InitializeHealth(config.Health);
        InitializeProjectileSpawner(projectileFactory, config.ProjectileConfig);
    }

    private void FixedUpdate()
    {
        if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            HandleInput();
    }

    private void HandleInput()
    {
        float xPos = _input.Direction.x / Screen.width;
        _body.MovePosition(new Vector3(Mathf.Lerp(_movementRangeMin, _movementRangeMax, xPos), 0.0f, 0.0f));
    }

    private void InitializeHealth(int health)
    {
        _health = new PlayerHealth(health);
        _health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        var fx = Instantiate(_prefabExplosion);
        fx.transform.position = transform.position;
    }

    private void InitializeProjectileSpawner(ProjectileFactory projectileFactory, ConfigContainer.ProjectileConfig projectileConfig)
    {
        ProjectileSpawner projectileSpawner = new ProjectileSpawner(projectileFactory);
        projectileSpawner.StartSpawning(EntityType.Player, projectileConfig, _projectileSpawnLocation).Forget();
    }

    public void AddPowerUp(PowerUp.PowerUpType type)
    {
        if (type.Equals(PowerUp.PowerUpType.FIRE_RATE))
        {
            //TODO: UPDATE IN PROJECTILE SPAWNER
            //_fireInterval *= 0.9f;
        }
    }
}
