using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;

public abstract class BaseFactory<TObject> : ILoadUnit<string> where TObject : Object
{
    private TObject _loadedObject;

    public UniTask Load(string resourcePath)
    {
        _loadedObject = Resources.Load<TObject>(resourcePath);

        if (_loadedObject == null)
        {
            Debug.LogError($"Failed to load object from Resources. Ensure the path '{resourcePath}' is correct and the asset exists.");
        }

        return UniTask.CompletedTask;
    }

    protected TObject CreateObject()
    {
        TObject instantiatedObject = Object.Instantiate(_loadedObject);

        return instantiatedObject;
    }
}