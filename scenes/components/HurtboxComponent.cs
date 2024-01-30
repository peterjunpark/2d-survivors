using Godot;

namespace TwoDSurvivors.Components;

public partial class HurtboxComponent : Area2D
{
    [Export]
    public HealthComponent HealthComponent { get; private set; }

    public override void _Ready()
    {
        AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area2D otherArea)
    {
        // Don't do anything if other Area2D is not a hitbox.
        if (otherArea is not HitboxComponent hitboxComponent) return;

        if (HealthComponent is null) return;

        HealthComponent.Damage(hitboxComponent.Damage);
    }
}
