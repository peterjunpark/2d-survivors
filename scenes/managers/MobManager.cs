using Godot;
using TwoDSurvivors.GameObjects;

namespace TwoDSurvivors.Managers;

public partial class MobManager : Node
{
    // Game viewport is 640 x 320.
    // We spawn mobs in a 330px radius around the player.
    private const int SpawnRadius = 330;

    [Export]
    private PackedScene BasicMobScene;
    public Player Player { get; private set; }

    public override void _Ready()
    {
        GetNode<Timer>("Timer").Timeout += HandleTimerTimeout;
        Player = (Player)GetTree().GetFirstNodeInGroup("player");
    }

    private void HandleTimerTimeout()
    {
        if (Player is null)
        {
            return;
        }

        // Instantiate mob.
        BasicMob mob = (BasicMob)BasicMobScene.Instantiate();
        GetParent().AddChild(mob);

        mob.GlobalPosition = GetSpawnPosition();
    }

    // This method makes sure that mobs are spawn in-bounds of the arena,
    // but outside of the player camera.
    private Vector2 GetSpawnPosition()
    {
        if (Player is null)
        {
            return Vector2.Zero;
        }

        Vector2 randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0.0f, Mathf.Tau));
        Vector2 spawnPosition = Vector2.Zero;

        // We'll check up to 4 rays until we find one that is not colliding with any terrain.
        for (int i = 0; i < 4; i++)
        {
            // The spawn position that we're considering in this iteration.
            spawnPosition = Player.GlobalPosition + (randomDirection * SpawnRadius);

            // Create a raycast query using the player position and the spawn position as the start and end points of the ray.
            PhysicsRayQueryParameters2D queryParameters = PhysicsRayQueryParameters2D.Create( // TODO: learn more about raycasting?
                Player.GlobalPosition,
                spawnPosition,
                1 // at terrain layer bit 0 (value 1)
            );

            // See if the ray intersects with terrain.
            Godot.Collections.Dictionary result = GetTree()
                .Root.World2D.DirectSpaceState.IntersectRay(queryParameters);

            // No intersections.
            if (result.Count == 0)
            {
                break;
            }

            // Not empty. Change angle randomDirection by 90 degrees and try again.
            randomDirection = randomDirection.Rotated(Mathf.Tau / 4);
        }

        return spawnPosition;
    }
}
