using System;
using Godot;
using TwoDSurvivors.GameObjects;

namespace TwoDSurvivors.Components;

public partial class XPVialDropComponent : Node
{
    [Export] public PackedScene XPVialScene { get; private set; }
    [Export] public HealthComponent HPComponent { get; private set; }
    [Export(PropertyHint.Range, "0, 1")]
    public float DropChance { get; private set; } = 0.5f;

    public override void _Ready()
    {
        HPComponent.Death += HandleDeath;
    }

    private void HandleDeath()
    {
        GD.Print("i'm dead");
        if (GD.Randf() > DropChance) return;

        if (XPVialScene is null) return;

        if (Owner is not Node2D) return;

        // Get position of mob.
        Vector2 spawnPosition = ((Node2D)Owner).GlobalPosition;
        // Instantiate xp.
        XPVial xpVialInstance = (XPVial)XPVialScene.Instantiate();
        // Add it to the scene tree as child of owner's parent (main).
        Owner.GetParent().AddChild(xpVialInstance);
        // Change global position to mob's posiiton.
        xpVialInstance.GlobalPosition = spawnPosition;
    }

}
