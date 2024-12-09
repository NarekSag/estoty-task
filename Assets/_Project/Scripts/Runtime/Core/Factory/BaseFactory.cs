using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using UnityEngine;

public abstract class BaseFactory<TObject> : ILoadUnit<string> where TObject : Object
{
    private TObject _loadedObject;
    private ObjectPool<TObject> _objectPool;

    public UniTask Load(string resourcePath)
    {
        _loadedObject = ResourceLoader.Load<TObject>(resourcePath);

        _objectPool = new ObjectPool<TObject>(_loadedObject);

        return UniTask.CompletedTask;
    }

    protected TObject CreateObject()
    {
        return _objectPool.Get();
    }

    public virtual void ReturnObject(TObject obj)
    {
        _objectPool.Return(obj);
    }
}