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
    private readonly EnemySpawner _enemySpawner;

    public CoreFlow(LoadingService loadingService,
        ConfigContainer configContainer,
        CoreController coreController,
        PlayerFactory playerFactory,
        EnemyFactory enemyFactory,
        EnemySpawner enemySpawner)
    {
        _loadingService = loadingService;
        _configContainer = configContainer;

        _coreController = coreController;
        _playerFactory = playerFactory;
        _enemyFactory = enemyFactory;
        _enemySpawner = enemySpawner;
    }

    public async void Start()
    {
        await _loadingService.BeginLoading(_playerFactory, RuntimeConstants.Resources.Player);
        await _loadingService.BeginLoading(_enemyFactory, RuntimeConstants.Resources.Enemy);

        await _loadingService.BeginLoading(_enemySpawner);

        await _loadingService.BeginLoading(_coreController, _configContainer);
    }
}
