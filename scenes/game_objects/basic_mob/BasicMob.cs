using Godot;
using TwoDSurvivors.Components;

namespace TwoDSurvivors.GameObjects;
public partial class BasicMob : CharacterBody2D
{
    public HealthComponent Health { get; private set; }
    public Area2D Hurtbox { get; private set; }
    private const int MaxSpeed = 69;

    // // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Health = GetNode<HealthComponent>("HealthComponent");
        Hurtbox = GetNode<Area2D>("HurtboxComponent");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 direction = GetDirectionToPlayer();
        Velocity = direction * MaxSpeed;
        _ = MoveAndSlide();
        GD.Print(Health.CurrentHealth);
    }

    private Vector2 GetDirectionToPlayer()
    {
        Player playerNode = (Player)GetTree().GetFirstNodeInGroup("player");

        return playerNode is null
            ? Vector2.Zero
            : (playerNode.GlobalPosition - GlobalPosition).Normalized();
    }
}
