public class PlayerFactory : BaseFactory<PlayerController>
{
    private readonly ProjectileFactory _projectileFactory;

    public PlayerFactory(ProjectileFactory projectileFactory)
    {
        _projectileFactory = projectileFactory;
    }

    public PlayerController CreatePlayer(ConfigContainer.PlayerConfig playerConfig)
    {
        PlayerController playerObject = CreateObject();

        if(playerObject != null)
        {
            playerObject.Initialize(playerConfig, GetPlayerInput(), _projectileFactory);
        }

        return playerObject;
    }

    private IInput GetPlayerInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        return new MouseInput();
#elif UNITY_IOS || UNITY_ANDROID
        return new TouchInput();
#endif
    }
}