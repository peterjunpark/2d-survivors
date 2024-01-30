using System;
using Godot;
using TwoDSurvivors.Autoload;

namespace TwoDSurvivors.Managers;

public partial class XPManager : Node
{
    public int CurrentLevel { get; private set; } = 1;
    public float CurrentXP { get; private set; } = 0;
    public float TargetXP { get; private set; } = 5;
    private const float TargetXPGrowth = 5;

    [Signal]
    public delegate void XPUpdatedEventHandler(float currentXP, float targetXP);
    private GameEvents GameEvents;

    public override void _Ready()
    {
        GameEvents = GetNode<GameEvents>("/root/GameEvents");
        GameEvents.XPVialCollected += HandleXPVialCollected;
    }

    public void AddXP(float number)
    {
        CurrentXP += number;

        EmitSignal(SignalName.XPUpdated, CurrentXP, TargetXP);

        if (CurrentXP >= TargetXP)
        {
            CurrentLevel++;
            TargetXP += TargetXPGrowth;
            CurrentXP = 0;
        }
    }

    private void HandleXPVialCollected(float number)
    {
        AddXP(number);
    }
}
