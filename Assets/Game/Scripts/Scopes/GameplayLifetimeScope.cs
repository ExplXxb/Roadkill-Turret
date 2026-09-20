using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // потрібно комусь
        builder.RegisterComponentInHierarchy<GameFlowController>();
        builder.RegisterComponentInHierarchy<Car>();

        // кому потрібні ін'єкції
        builder.RegisterComponentInHierarchy<LevelController>();
        builder.RegisterComponentInHierarchy<CarMovement>();
        builder.RegisterComponentInHierarchy<TurretShooting>();
        builder.RegisterComponentInHierarchy<TurretAiming>();
    }
}
