using Scripts.Runtime.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class BootstrapScope : LifetimeScope
{
    protected override void Awake()
    {
        DontDestroyOnLoad(this);
        base.Awake();
    }

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<LoadingService>(Lifetime.Scoped);
        builder.Register<ConfigContainer>(Lifetime.Singleton);

        builder.RegisterEntryPoint<BootstrapFlow>();
    }
}