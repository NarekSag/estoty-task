public class EnemyFactory : BaseFactory<EnemyController>
{
    public EnemyController CreateEnemy(ConfigContainer.EnemyConfig enemyConfig)
    {
        EnemyController enemyObject = CreateObject();

        if (enemyObject == null) return null;

        enemyObject.Initialize(enemyConfig, ObjectPool);
        return enemyObject;
    }
}