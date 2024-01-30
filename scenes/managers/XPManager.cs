using Godot;

namespace TwoDSurvivors.XPManager;

public partial class XPManager : Node
{
    private GameEvents.GameEvents GameEvents;

    public float CurrentXP { get; private set; } = 0.0f;

    public override void _Ready()
    {
        GameEvents = GetNode<GameEvents.GameEvents>("/root/GameEvents");
        GameEvents.XPVialCollected += HandleExperienceVialCollected;
    }

    public void AddXP(float number)
    {
        CurrentXP += number;
        GD.Print(CurrentXP);
    }

    private void HandleExperienceVialCollected(float number)
    {
        AddXP(number);
    }
}
