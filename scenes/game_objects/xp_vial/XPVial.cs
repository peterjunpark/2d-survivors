using Godot;

namespace TwoDSurvivors.XPVial;

public partial class XPVial : Node2D
{
    private GameEvents.GameEvents GameEvents;

    public Area2D Area2D { get; private set; }

    public override void _Ready()
    {
        GameEvents = GetNode<GameEvents.GameEvents>("/root/GameEvents");

        Area2D = GetNode<Area2D>("Area2D");
        Area2D.AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area2D otherArea)
    {
        GameEvents.EmitXPVialCollected(1);
        QueueFree();
    }

}
