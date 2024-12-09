using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Object
{
    private Queue<T> _pool = new Queue<T>();
    private T _prefab;

    public ObjectPool(T prefab, Transform parent = null, int initialSize = 0)
    {
        _prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            Return(Object.Instantiate(_prefab, parent));
        }
    }

    public T Get()
    {
        if(_pool.Count <= 0)
        {
            _pool.Enqueue(Object.Instantiate(_prefab));
        }

        return _pool.Dequeue();
    }

    public void Return(T obj)
    {
        _pool.Enqueue(obj);
    }
}