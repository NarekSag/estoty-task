using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;

public class ApplicationConfigurationUnit : ILoadUnit
{
    public UniTask Load()
    {
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
        return UniTask.CompletedTask;
    }
}
