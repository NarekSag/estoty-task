public class EnemyFactory : BaseFactory<EnemyController>
{
    public EnemyController CreateEnemy(ConfigContainer.EnemyConfig enemyConfig)
    {
        EnemyController enemyObject = CreateObject();

        if (enemyObject != null)
        {
            enemyObject.Initialize(enemyConfig);
        }

        return enemyObject;
    }
}