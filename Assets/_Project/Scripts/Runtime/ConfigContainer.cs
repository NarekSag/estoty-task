using Cysharp.Threading.Tasks;
using Scripts.Runtime.Utilities;
using System;
using UnityEngine;

public sealed class ConfigContainer : ILoadUnit
{
    public CoreConfigContainer Core;

    public UniTask Load()
    {
        // Assuming the configuration data is fetched from a backend service or Firebase,  
        // rather than being stored locally as a text file in the Resources folder.

        var asset = Resources.Load<TextAsset>(RuntimeConstants.Configs.ConfigFileName);
        JsonUtility.FromJsonOverwrite(asset.text, this);
        return UniTask.CompletedTask;
    }

    [Serializable]
    public class CoreConfigContainer
    {
        public PlayerConfig PlayerConfig;
    }

    [Serializable]
    public class PlayerConfig
    {
        public int Health;
        public float FireInterval;
        public float MovementRangeMin;
        public float MovementRangeMax;
    }
}
