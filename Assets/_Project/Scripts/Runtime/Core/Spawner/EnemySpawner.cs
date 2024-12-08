using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using System;
using UnityEngine;
using VContainer.Unity;

public class EnemySpawner : ILoadUnit
{
    private readonly EnemyFactory _enemyFactory;

    private Transform _parent;

    public EnemySpawner(EnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }

    public UniTask Load()
    {
        GameObject parentObject = new GameObject("EnemySpawner");
        parentObject.transform.position = new Vector3(0, 14, 0);
        _parent = parentObject.transform;

        return UniTask.CompletedTask;
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
        Enemy enemy = _enemyFactory.CreateEnemy(config);

        SetParent(ref enemy);
        SetRandomPosition(ref enemy, config.HorizontalSpawnRange);
    }

    private void SetParent(ref Enemy enemy)
    {
        enemy.transform.SetParent(_parent);
        enemy.transform.localPosition = Vector3.zero;
    }

    private void SetRandomPosition(ref Enemy enemy, float horizontalSpawnRange)
    {
        enemy.transform.position = new Vector3(GetRandomHorizontalPosition(horizontalSpawnRange), enemy.transform.position.y, 0);
    }

    private float GetRandomHorizontalPosition(float horizontalSpawnRange)
    {
        return UnityEngine.Random.Range(-horizontalSpawnRange, horizontalSpawnRange);
    }
}
