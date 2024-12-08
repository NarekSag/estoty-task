public class EnemyFactory : BaseFactory<Enemy>
{
    public Enemy CreateEnemy(ConfigContainer.EnemyConfig enemyConfig)
    {
        Enemy enemyObject = CreateObject(RuntimeConstants.Resources.Enemy);

        if (enemyObject != null)
        {
            enemyObject.Initialize(enemyConfig);
        }

        return enemyObject;
    }
}