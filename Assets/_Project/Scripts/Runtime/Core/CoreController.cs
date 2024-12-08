using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;

public class CoreController : ILoadUnit<ConfigContainer>
{
    private readonly PlayerFactory _playerFactory;

    public CoreController(PlayerFactory playerFactory)
    {
        _playerFactory = playerFactory;
    }

    public UniTask Load(ConfigContainer config)
    {
        _playerFactory.CreatePlayer(config.Core.PlayerConfig);

        return UniTask.CompletedTask;
    }
}
