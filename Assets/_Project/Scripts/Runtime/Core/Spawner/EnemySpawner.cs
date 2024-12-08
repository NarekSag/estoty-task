using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class EnemySpawner
{
    private readonly EnemyFactory _enemyFactory;

    private Transform _parent;

    public EnemySpawner(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        SetupSpawnerParent();
    }

    public async UniTask StartSpawning(ConfigContainer.EnemyConfig config)
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(config.SpawnInterval));

            SpawnEnemy(config);
        }
    }

    private void SpawnEnemy(ConfigContainer.EnemyConfig config)
    {
        EnemyController enemy = _enemyFactory.CreateEnemy(config);

        SetParent(ref enemy);
        SetRandomPosition(ref enemy, config.HorizontalSpawnRange);
    }

    private void SetParent(ref EnemyController enemy)
    {
        enemy.transform.SetParent(_parent);
        enemy.transform.localPosition = Vector3.zero;
    }

    private void SetRandomPosition(ref EnemyController enemy, float horizontalSpawnRange)
    {
        enemy.transform.position = new Vector3(GetRandomHorizontalPosition(horizontalSpawnRange), enemy.transform.position.y, 0);
    }

    private float GetRandomHorizontalPosition(float horizontalSpawnRange)
    {
        return UnityEngine.Random.Range(-horizontalSpawnRange, horizontalSpawnRange);
    }

    // To keep the inspector organized, we will create a EnemySpawner object 
    // so that all enemies are stored within this parent object.
    private void SetupSpawnerParent()
    {
        GameObject parentObject = new GameObject("EnemySpawner");
        parentObject.transform.position = new Vector3(0, 14, 0);
        _parent = parentObject.transform;
    }
}
