using System;
using Godot;

namespace TwoDSurvivors.GameObjects;

public partial class GameCamera : Camera2D
{
    public Vector2 TargetPosition { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        MakeCurrent();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        GetPlayerTargetPosition();
        // LERP between current position to target position for smoother camera movement.
        GlobalPosition = GlobalPosition.Lerp(
            TargetPosition,
            1.0f - (float)Math.Exp(-delta * 20)
        );
    }

    private void GetPlayerTargetPosition()
    {
        Godot.Collections.Array<Node> playerNodes = GetTree().GetNodesInGroup("player");

        if (playerNodes.Count > 0)
        {
            Player playerNode = (Player)playerNodes[0];
            // Set target position to the player's current position.
            TargetPosition = playerNode.GlobalPosition;
        }
    }
}
