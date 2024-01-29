using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Godot;

namespace TwoDSurvivors.SwordAbility
{
    public partial class SwordAbilityController : Node
    {
        private const int MaxRange = 150;

        [Export]
        public PackedScene SwordAbility { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetNode<Timer>("Timer").Timeout += HandleTimerTimeout;
        }

        public void HandleTimerTimeout()
        {
            Player.Player player = (Player.Player)GetTree().GetFirstNodeInGroup("player");
            if (player is null) return;

            // TODO: These type conversions might be suboptimal due to marshalling.

            // Get all spawned mobs.
            Godot.Collections.Array<Node> mobs = GetTree().GetNodesInGroup("mob");

            if (mobs.Count <= 0) return;

            // Sort them by proximity to the player.
            mobs = new Godot.Collections.Array<Node>(mobs.OrderBy(mob => ((Node2D)mob).GlobalPosition.DistanceSquaredTo(player.GlobalPosition)));

            // Instantiate sword in the main scene...
            Node2D swordInstance = (Node2D)SwordAbility.Instantiate();
            player.GetParent().AddChild(swordInstance);
            // on top of the nearest mob.
            swordInstance.GlobalPosition = ((Node2D)mobs[0]).GlobalPosition;
        }
    }
}
