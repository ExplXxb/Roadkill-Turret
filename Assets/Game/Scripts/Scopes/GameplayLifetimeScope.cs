using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameFlowController>();

        builder.RegisterComponentInHierarchy<Car>();

        builder.RegisterComponentInHierarchy<LevelController>();
        builder.RegisterComponentInHierarchy<ResultScreenUI>();
    }
}
