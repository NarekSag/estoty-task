public class EnemyFactory : BaseFactory<EnemyController>
{
    private readonly ProjectileFactory _projectileFactory;

    public EnemyFactory(ProjectileFactory projectileFactory)
    {
        _projectileFactory = projectileFactory;
    }

    public EnemyController CreateEnemy(ConfigContainer.EnemyConfig enemyConfig)
    {
        EnemyController enemyObject = CreateObject();

        if (enemyObject != null)
        {
            enemyObject.Initialize(enemyConfig, _projectileFactory);
        }

        return enemyObject;
    }
}