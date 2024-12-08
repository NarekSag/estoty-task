using Scripts.Runtime.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

public class CoreFlow : IStartable
{
    private readonly LoadingService _loadingService;
    private readonly ConfigContainer _configContainer;

    private readonly CoreController _coreController;

    private readonly PlayerFactory _playerFactory;
    private readonly EnemyFactory _enemyFactory;
    private readonly ProjectileFactory _projectileFactory;

    private readonly EnemySpawner _enemySpawner;

    public CoreFlow(LoadingService loadingService,
        ConfigContainer configContainer,
        CoreController coreController,
        PlayerFactory playerFactory,
        EnemyFactory enemyFactory,
        ProjectileFactory projectileFactory,
        EnemySpawner enemySpawner)
    {
        _loadingService = loadingService;
        _configContainer = configContainer;

        _coreController = coreController;

        _playerFactory = playerFactory;
        _enemyFactory = enemyFactory;
        _projectileFactory = projectileFactory;

        _enemySpawner = enemySpawner;
    }

    public async void Start()
    {
        await _loadingService.BeginLoading(_playerFactory, RuntimeConstants.Resources.Entities.Player);
        await _loadingService.BeginLoading(_enemyFactory, RuntimeConstants.Resources.Entities.Enemy);

        await _loadingService.BeginLoading(_projectileFactory, RuntimeConstants.Resources.Projectiles.Player);
        //await _loadingService.BeginLoading(_projectileFactory, RuntimeConstants.Resources.Projectiles.Enemy);

        await _loadingService.BeginLoading(_enemySpawner);

        await _loadingService.BeginLoading(_coreController, _configContainer);
    }
}
