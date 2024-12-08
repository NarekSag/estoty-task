using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;

public class CoreController : ILoadUnit<ConfigContainer>
{
    private readonly PlayerFactory _playerFactory;
    private readonly EnemySpawner _enemySpawner;

    public CoreController(PlayerFactory playerFactory,
        EnemySpawner enemySpawner)
    {
        _playerFactory = playerFactory;
        _enemySpawner = enemySpawner;
    }

    public UniTask Load(ConfigContainer config)
    {
        _playerFactory.CreatePlayer(config.Core.PlayerConfig);
        _enemySpawner.StartSpawning(config.Core.EnemyConfig).Forget();

        return UniTask.CompletedTask;
    }
}
