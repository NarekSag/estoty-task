using UnityEngine;
using System;

public static class ResourceLoader
{
    public static T Load<T>(string path) where T : UnityEngine.Object
    {
        T resource = Resources.Load<T>(path);

        if (resource == null)
        {
            throw new InvalidOperationException($"Failed to load {typeof(T).Name} from path: {path}");
        }

        return resource;
    }
}