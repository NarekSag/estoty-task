public class PlayerFactory : BaseFactory<Player>
{
    public Player CreatePlayer(ConfigContainer.PlayerConfig playerConfig)
    {
        Player playerObject = CreateObject(RuntimeConstants.Resources.Player);

        if(playerObject != null)
        {
            playerObject.Initialize(playerConfig);
        }

        return playerObject;
    }
}