using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;

public class CoreController : ILoadUnit<ConfigContainer>
{
    private readonly PlayerFactory _playerFactory;
    private readonly ProjectileFactory _projectileFactory;

    private readonly EnemySpawner _enemySpawner;

    public CoreController(PlayerFactory playerFactory,
        ProjectileFactory projectileFactory,
        EnemySpawner enemySpawner)
    {
        _playerFactory = playerFactory;
        _projectileFactory = projectileFactory;

        _enemySpawner = enemySpawner;
    }

    public UniTask Load(ConfigContainer config)
    {
        _playerFactory.CreatePlayer(config.Core.PlayerConfig);
        _enemySpawner.StartSpawning(config.Core.EnemyConfig).Forget();

        return UniTask.CompletedTask;
    }
}
