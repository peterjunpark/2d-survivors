using Godot;
using TwoDSurvivors.Autoload;

namespace TwoDSurvivors.Managers;

public partial class XPManager : Node
{
    public int CurrentLevel { get; private set; } = 1;
    public float CurrentXP { get; private set; }
    public float TargetXP { get; private set; } = 5;
    private const float TargetXPGrowth = 5;

    [Signal]
    public delegate void XPUpdatedEventHandler(float currentXP, float targetXP);

    [Signal]
    public delegate void LevelUpEventHandler(int newLevel);
    private GameEvents GameEvents;

    public override void _Ready()
    {
        GameEvents = GetNode<GameEvents>("/root/GameEvents");
        GameEvents.XPVialCollected += HandleXPVialCollected;
    }

    public void AddXP(float number)
    {
        CurrentXP += number;

        _ = EmitSignal(SignalName.XPUpdated, CurrentXP, TargetXP);

        if (CurrentXP >= TargetXP)
        {
            CurrentLevel++;
            TargetXP += TargetXPGrowth;
            CurrentXP = 0;
            _ = EmitSignal(SignalName.LevelUp, CurrentLevel);
        }
    }

    private void HandleXPVialCollected(float number)
    {
        AddXP(number);
    }
}
