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

    public CoreFlow(LoadingService loadingService,
        ConfigContainer configContainer,
        CoreController coreController,
        PlayerFactory playerFactory)
    {
        _loadingService = loadingService;
        _configContainer = configContainer;

        _coreController = coreController;
        _playerFactory = playerFactory;
    }

    public async void Start()
    {
        await _loadingService.BeginLoading(_playerFactory, RuntimeConstants.Resources.Player);

        await _loadingService.BeginLoading(_coreController, _configContainer);
    }
}
