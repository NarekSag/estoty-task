using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyController : EntityController
{
    [SerializeField] private PowerUp _prefabPowerUp;
    private float _powerUpSpawnChance = 0.1f;

    private ObjectPool<EnemyController> _pool;

    private BoundsHandler _boundsHandler;
    private EnemyMovement _movement;
    private EnemyProjectileHandler _projectileHandler;
    private EnemyPowerUp _powerUp;

    public EnemyProjectileHandler ProjectileHandler => _projectileHandler;

    private bool _isOutsideBounds;

    protected override void Awake()
    {
        base.Awake();
        InitializeBoundsHandler();
        InitializePowerUp();
    }

    public void Initialize(ConfigContainer.EnemyConfig config, ObjectPool<EnemyController> pool)
    {
        base.Initialize(config.Health);
        InitializeMovement(config.Speed);
        InitializeProjectileHandler();

        _pool = pool;
        _powerUpSpawnChance = config.PowerUpSpawnChance;

        _isOutsideBounds = false;

        Health.OnDeath += ReturnToPool;
    }

    protected override void FixedUpdate()
    {
        if (!Health.IsAlive || _isOutsideBounds) return;

        _boundsHandler.CheckBounds();
        _movement.Move();
    }

    private void InitializeBoundsHandler()
    {
        _boundsHandler = new BoundsHandler(transform);
        _boundsHandler.OnOutsideBounds += HandleOutsideBounds;
    }

    private void InitializeMovement(float speed)
    {
        _movement = new EnemyMovement(Body, speed);
    }

    private void InitializeProjectileHandler()
    {
        _projectileHandler = new EnemyProjectileHandler();
    }

    private void InitializePowerUp()
    {
        _powerUp = new EnemyPowerUp(_prefabPowerUp);
    }

    private void HandleOutsideBounds()
    {
        _projectileHandler.Reset();
        ToggleEntityState(false);
        ReturnToPool();
        _isOutsideBounds = true;
    }

    protected override void HandleDeath()
    {
        _projectileHandler.Reset();
        _powerUp.Generate(_powerUpSpawnChance);
        base.HandleDeath();
    }

    private void ReturnToPool()
    {
        _pool?.Return(this);
        Health.OnDeath -= ReturnToPool;
    }
}