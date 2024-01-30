using System.Collections.Generic;
using Godot;
using TwoDSurvivors.Resources;

namespace TwoDSurvivors.Managers;

public class UpgradeInfo(AbilityUpgrade upgrade, int quantity)
{
    public AbilityUpgrade Upgrade { get; set; } = upgrade;
    public int Quantity { get; set; } = quantity;

    public override string ToString()
    {
        return $"{Upgrade} ({Quantity})";
    }
}

public partial class UpgradeManager : Node
{
    [Export]
    public Godot.Collections.Array<AbilityUpgrade> UpgradePool { get; private set; }

    [Export]
    private XPManager XPManager;

    private readonly Dictionary<string, UpgradeInfo> CurrentUpgrades = [];

    public override void _Ready()
    {
        XPManager.LevelUp += HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        AbilityUpgrade chosenUpgrade = UpgradePool.PickRandom();

        if (chosenUpgrade is null)
        {
            return;
        }

        bool hasUpgrade = CurrentUpgrades.ContainsKey(chosenUpgrade.Id);

        if (!hasUpgrade)
        {
            CurrentUpgrades[chosenUpgrade.Id] = new UpgradeInfo(chosenUpgrade, 1);
        }
        else
        {
            CurrentUpgrades[chosenUpgrade.Id].Quantity++;
        }
    }
}
