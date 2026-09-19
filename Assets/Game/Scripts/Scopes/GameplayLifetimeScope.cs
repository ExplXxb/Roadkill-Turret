using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // потрібно комусь
        builder.RegisterComponentInHierarchy<GameFlowController>();

        // кому потрібні ін'єкції
        builder.RegisterComponentInHierarchy<CarMovement>();
        builder.RegisterComponentInHierarchy<TurretShooting>();
    }
}
