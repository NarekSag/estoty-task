using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class CoreScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerFactory>(Lifetime.Scoped);
        builder.Register<EnemyFactory>(Lifetime.Scoped);
        builder.Register<ProjectileFactory>(Lifetime.Scoped);

        builder.Register<CoreController>(Lifetime.Scoped);

        builder.RegisterEntryPoint<CoreFlow>();
    }
}
