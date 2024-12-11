using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CoreScope : LifetimeScope
{
    [SerializeField] private CoreViewController viewController;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(viewController).AsSelf();

        builder.Register<PlayerFactory>(Lifetime.Scoped);
        builder.Register<EnemyFactory>(Lifetime.Scoped);
        builder.Register<ProjectileFactory>(Lifetime.Scoped);

        builder.Register<CoreController>(Lifetime.Scoped);

        builder.RegisterEntryPoint<CoreFlow>();
    }
}
