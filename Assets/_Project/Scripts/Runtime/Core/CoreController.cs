using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;

public class CoreController : ILoadUnit<ConfigContainer>
{
    private readonly PlayerFactory _playerFactory;
    private readonly EnemyFactory _enemyFactory;
    private readonly ProjectileFactory _projectileFactory;

    private EnemySpawner _enemySpawner;

    public CoreController(PlayerFactory playerFactory,
        EnemyFactory enemyFactory,
        ProjectileFactory projectileFactory)
    {
        _playerFactory = playerFactory;
        _enemyFactory = enemyFactory;
        _projectileFactory = projectileFactory;
    }

    public UniTask Load(ConfigContainer config)
    {
        _playerFactory.CreatePlayer(config.Core.PlayerConfig);

        _enemySpawner = new EnemySpawner(_enemyFactory);
        _enemySpawner.StartSpawning(config.Core.EnemyConfig).Forget();

        return UniTask.CompletedTask;
    }
}
