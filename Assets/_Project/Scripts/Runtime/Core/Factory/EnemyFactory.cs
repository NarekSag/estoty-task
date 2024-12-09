public class EnemyFactory : BaseFactory<EnemyController>
{
    public EnemyController CreateEnemy(ConfigContainer.EnemyConfig enemyConfig)
    {
        EnemyController enemyObject = CreateObject();

        if (enemyObject == null) return null;

        enemyObject.Initialize(enemyConfig);
        enemyObject.Health.OnDeath += () => ReturnObject(enemyObject); //TODO: Get rid of lambda
        enemyObject.gameObject.SetActive(true);

        return enemyObject;
    }
}