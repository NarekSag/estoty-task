using System;
using System.Linq;

public class EnemyPowerUp
{
    private PowerUp _prefabPowerUp;

    public EnemyPowerUp(PowerUp prefabPowerUp)
    {
        _prefabPowerUp = prefabPowerUp;
    }

    public void Generate(float chance)
    {
        if (UnityEngine.Random.value >= chance) return;

        var powerup = UnityEngine.Object.Instantiate(_prefabPowerUp); //TODO: POOL
        var types = Enum.GetValues(typeof(PowerUp.PowerUpType)).Cast<PowerUp.PowerUpType>().ToList();
        powerup.SetType(types[UnityEngine.Random.Range(0, types.Count)]);
    }
}
