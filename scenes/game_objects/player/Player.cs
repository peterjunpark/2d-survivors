using System;
using Godot;

namespace TwoDSurvivors.GameObjects;
public partial class Player : CharacterBody2D
{
    private const int MaxSpeed = 140;
    private const int Acceleration = 30;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() { }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 movementVector = GetMovementVector();
        Vector2 targetVelocity = movementVector.Normalized() * MaxSpeed;

        Velocity = Velocity.Lerp(targetVelocity,
            1.0f - (float)Math.Exp(-delta * Acceleration));
        // Tell the CharacterBody2D to move based on Velocity.
        _ = MoveAndSlide();
    }

    private static Vector2 GetMovementVector()
    {
        // With keyboard controls, ActionStrength is binary. It's 0 or 1.
        // On a joystick, it returns a float between 0 and 1.
        float xMovement =
            Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

        float yMovement =
            Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

        return new Vector2(xMovement, yMovement);
    }
}
