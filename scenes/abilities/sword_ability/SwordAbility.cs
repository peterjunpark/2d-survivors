using Godot;
using TwoDSurvivors.Components;

namespace TwoDSurvivors.Abilities;

public partial class SwordAbility : Node2D
{
    public HitboxComponent Hitbox { get; private set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Hitbox = GetNode<HitboxComponent>("HitboxComponent");
    }
}
