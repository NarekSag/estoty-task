using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _prefabExplosion;
    [SerializeField] private PowerUp _prefabPowerUp;

    private float _powerUpSpawnChance = 0.1f;
    private float _speed = 2.0f;

    private Rigidbody _body;

    private EnemyHealth _health;
    public EnemyHealth Health => _health;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    public void Initialize(ConfigContainer.EnemyConfig config, ProjectileFactory projectileFactory)
    {
        _powerUpSpawnChance = config.PowerUpSpawnChance;
        _speed = config.Speed;

        InitializeHealth(config.Health);
        //if(Random.value < 0.4f)
            InitializeProjectileSpawner(projectileFactory, config.ProjectileConfig);
    }

    private void FixedUpdate()
    {
        var p = _body.position;
        p += Vector3.down * (_speed * Time.deltaTime);
        _body.MovePosition(p);
    }

    private void InitializeHealth(int health)
    {
        _health = new EnemyHealth(health);
        _health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        var fx = Instantiate(_prefabExplosion);
        fx.transform.position = transform.position;

        if (Random.value < _powerUpSpawnChance)
        {
            var powerup = Instantiate(_prefabPowerUp);
            var types = Enum.GetValues(typeof(PowerUp.PowerUpType)).Cast<PowerUp.PowerUpType>().ToList();
            powerup.SetType(types[Random.Range(0, types.Count)]);
        }

        Destroy(gameObject);
    }

    private void InitializeProjectileSpawner(ProjectileFactory projectileFactory, ConfigContainer.ProjectileConfig projectileConfig)
    {
        ProjectileSpawner projectileSpawner = new ProjectileSpawner(projectileFactory);
        projectileSpawner.StartSpawning(EntityType.Enemy, projectileConfig, transform).Forget();
    }
}
