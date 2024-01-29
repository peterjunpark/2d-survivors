using Godot;

namespace TwoDSurvivors.BasicMob
{
    public partial class BasicMob : CharacterBody2D
    {
        private const int MaxSpeed = 75;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready() { }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
            Vector2 direction = GetDirectionToPlayer();
            Velocity = direction * MaxSpeed;
            _ = MoveAndSlide();
        }

        private Vector2 GetDirectionToPlayer()
        {
            Player.Player playerNode = (Player.Player)GetTree().GetFirstNodeInGroup("player");

            return playerNode == null
                ? Vector2.Zero
                : (playerNode.GlobalPosition - GlobalPosition).Normalized();
        }
    }
}
