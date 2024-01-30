using System;
using Godot;

namespace TwoDSurvivors.Components;

public partial class HealthComponent : Node
{
    [Export] public float MaxHealth { get; private set; } = 10;
    public float CurrentHealth { get; private set; }
    [Signal] public delegate void DeathEventHandler(bool dead);
    public bool Dead { get; private set; } = false;
    private Callable CheckDeathCallable;

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
        CheckDeathCallable = Callable.From(CheckDeath);
    }

    public void Damage(float damage)
    {
        CurrentHealth = Math.Max(0.0f, CurrentHealth - damage);
        // Defer call to next idle frame.
        CheckDeathCallable.CallDeferred();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0.0f)
        {
            // Emit signal that the owner of this HealthComponent is dead.
            EmitSignal(SignalName.Death, true);
            Owner.QueueFree();
        }
    }
}
