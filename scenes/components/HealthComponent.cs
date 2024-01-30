using System;
using Godot;

namespace TwoDSurvivors.Components;

public partial class HealthComponent : Node
{
    [Export]
    public float MaxHealth { get; private set; } = 10;
    public float CurrentHealth { get; private set; }

    [Signal]
    public delegate void DeathEventHandler(bool dead);
    public bool Dead { get; private set; } = false;

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float damage)
    {
        CurrentHealth = Math.Max(0.0f, CurrentHealth - damage);

        if (CurrentHealth <= 0.0f)
        {
            // Emit signal that the owner of this HealthComponent is dead.
            EmitSignal(SignalName.Death, true);
            Owner.QueueFree();
        }
    }
}
