using Godot;

namespace TwoDSurvivors.MobManager
{
    public partial class MobManager : Node
    {
        // Game viewport is 640 x 320.
        // We spawn mobs in a 330px radius around the player.
        private const int SpawnRadius = 330;

        [Export]
        public PackedScene BasicMobScene { get; private set; }

        public override void _Ready()
        {
            GetNode<Timer>("Timer").Timeout += HandleTimerTimeout;
        }

        private void HandleTimerTimeout()
        {
            var player = (Player.Player)GetTree().GetFirstNodeInGroup("player");

            if (player is null) return;

            Vector2 randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0.0f, Mathf.Tau));
            Vector2 spawnPosition = player.GlobalPosition + (randomDirection * SpawnRadius);

            // Instantiate mob.
            var mob = (BasicMob.BasicMob)BasicMobScene.Instantiate();
            GetParent().AddChild(mob);
            mob.GlobalPosition = spawnPosition;
        }
    }

}
