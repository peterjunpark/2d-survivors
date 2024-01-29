using Godot;

namespace TwoDSurvivors.SwordAbility
{
    public partial class SwordAbilityController : Node
    {
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

            if (player == null)
            {
                return;
            }

            // Instantiate sword in parent of player (main scene)
            Node2D swordInstance = (Node2D)SwordAbility.Instantiate();
            player.GetParent().AddChild(swordInstance);
            swordInstance.GlobalPosition = player.GlobalPosition;
        }
    }
}
