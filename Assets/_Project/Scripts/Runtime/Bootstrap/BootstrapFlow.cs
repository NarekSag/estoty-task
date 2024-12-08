using Scripts.Runtime.Utilities;
using VContainer.Unity;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class BootstrapFlow : IStartable
{
    private readonly LoadingService _loadingService;

    public BootstrapFlow(LoadingService loadingService)
    {
        _loadingService = loadingService;
    }

    public async void Start()
    {
        await _loadingService.BeginLoading(new ApplicationConfigurationUnit());

        UnitySceneManager.LoadScene("GameScene"); // TODO: Add to constants
    }
}