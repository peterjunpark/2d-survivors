using System;
using System.Linq;
using Godot;
using TwoDSurvivors.GameObjects;

namespace TwoDSurvivors.Abilities;
public partial class SwordAbilityController : Node
{
    private const int MaxRange = 80;
    private const int Damage = 5;

    [Export]
    public PackedScene SwordAbility { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Timer>("Timer").Timeout += HandleTimerTimeout;
    }

    private void HandleTimerTimeout()
    {
        Player player = (Player)GetTree().GetFirstNodeInGroup("player");

        if (player is null) return;

        // TODO: These type conversions might be suboptimal due to marshalling.

        // Get all spawned mobs.
        Godot.Collections.Array<Node> mobs = GetTree().GetNodesInGroup("mob");

        if (mobs.Count <= 0) return;

        // Sort them by proximity to the player.
        mobs = new Godot.Collections.Array<Node>(mobs.OrderBy(mob => ((Node2D)mob).GlobalPosition.DistanceSquaredTo(player.GlobalPosition)));

        // Get closest mob and cast to BasicMob to access its properties/methods.
        BasicMob closestMob = (BasicMob)mobs[0];

        // Make sure closestMob (target) is in range
        if (closestMob.GlobalPosition.DistanceSquaredTo(player.GlobalPosition) > Math.Pow(MaxRange, 2)) return;

        // Instantiate sword in the main scene...
        SwordAbility swordInstance = (SwordAbility)SwordAbility.Instantiate();
        player.GetParent().AddChild(swordInstance);
        swordInstance.Hitbox.Damage = Damage;

        // Rotate sword and instantiate with a slight offset from the target mob.
        swordInstance.GlobalPosition = closestMob.GlobalPosition + Vector2.Right.Rotated((float)GD.RandRange(0.0f, Math.Tau)) * 4;

        // Point the sword's hitbox in the direction of the mob.
        Vector2 mobDirection = closestMob.GlobalPosition - swordInstance.GlobalPosition;
        swordInstance.Rotation = mobDirection.Angle();
    }
}
