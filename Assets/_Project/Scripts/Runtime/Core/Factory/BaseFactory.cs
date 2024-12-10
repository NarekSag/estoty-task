using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;

public abstract class BaseFactory<TObject> : ILoadUnit<string> where TObject : Object
{
    private TObject _loadedObject;
    protected ObjectPool<TObject> ObjectPool;

    public UniTask Load(string resourcePath)
    {
        _loadedObject = ResourceLoader.Load<TObject>(resourcePath);

        ObjectPool = new ObjectPool<TObject>(_loadedObject);

        return UniTask.CompletedTask;
    }

    protected TObject CreateObject()
    {
        return ObjectPool.Get();
    }

    protected virtual void ReturnObject(TObject obj)
    {
        ObjectPool.Return(obj);
    }
}