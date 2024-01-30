using Godot;
using TwoDSurvivors.Components;

namespace TwoDSurvivors.GameObjects;
public partial class BasicMob : CharacterBody2D
{
    public HealthComponent Health { get; private set; }
    private const int MaxSpeed = 69;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Health = GetNode<HealthComponent>("HealthComponent");
        GetNode<Area2D>("Area2D").AreaEntered += HandleAreaEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 direction = GetDirectionToPlayer();
        Velocity = direction * MaxSpeed;
        _ = MoveAndSlide();
    }

    private Vector2 GetDirectionToPlayer()
    {
        Player playerNode = (Player)GetTree().GetFirstNodeInGroup("player");

        return playerNode is null
            ? Vector2.Zero
            : (playerNode.GlobalPosition - GlobalPosition).Normalized();
    }

    private void HandleAreaEntered(Area2D otherArea)
    {
        Health.Damage(100);
        QueueFree();
    }
}
