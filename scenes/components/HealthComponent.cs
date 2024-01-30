using System;
using Godot;

namespace TwoDSurvivors.Components;

public partial class HealthComponent : Node
{
    [Export] public int MaxHealth { get; private set; } = 10;
    public int CurrentHealth { get; private set; }
    [Signal] public delegate void DeathEventHandler();
    public bool Dead { get; private set; } = false;
    private Callable CheckDeathCallable;

    public override void _Ready()
    {
        CurrentHealth = MaxHealth;
        CheckDeathCallable = Callable.From(CheckDeath);
    }

    public void Damage(int damage)
    {
        GD.Print("oof");
        CurrentHealth = Math.Max(0, CurrentHealth - damage);
        // Defer call to next idle frame.
        CheckDeathCallable.CallDeferred();
    }

    private void CheckDeath()
    {
        if (CurrentHealth <= 0)
        {
            // Emit signal that the owner of this HealthComponent is dead.
            EmitSignal(SignalName.Death);
            Owner.QueueFree();
        }
    }
}
