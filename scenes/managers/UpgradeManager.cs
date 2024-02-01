using System;
using System.Collections.Generic;
using Godot;
using TwoDSurvivors.Resources;
using TwoDSurvivors.UI;

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

    [Export]
    private PackedScene AbilityUpgradeScreenScene;

    private readonly Dictionary<string, UpgradeInfo> CurrentUpgrades = [];

    public override void _Ready()
    {
        XPManager.LevelUp += HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        AbilityUpgrade chosenUpgrade = UpgradePool.PickRandom();
        GD.Print(chosenUpgrade);

        if (chosenUpgrade is null)
        {
            return;
        }

        // Instantiate upgrade screen.
        AbilityUpgradeScreen abilityUpgradeScreenInstance = (AbilityUpgradeScreen)
            AbilityUpgradeScreenScene.Instantiate();
        // Set it as a child.
        AddChild(abilityUpgradeScreenInstance);

        abilityUpgradeScreenInstance.SetAbilityUpgrades([chosenUpgrade]);
        abilityUpgradeScreenInstance.AbilityUpgradeSelected += HandleAbilityUpgradeSelected;
    }

    private void HandleAbilityUpgradeSelected(AbilityUpgrade abilityUpgrade)
    {
        ApplyUpgrade(abilityUpgrade);
    }

    private void ApplyUpgrade(AbilityUpgrade abilityUpgrade)
    {
        bool hasUpgrade = CurrentUpgrades.ContainsKey(abilityUpgrade.Id);

        if (!hasUpgrade)
        {
            CurrentUpgrades[abilityUpgrade.Id] = new UpgradeInfo(abilityUpgrade, 1);
        }
        else
        {
            CurrentUpgrades[abilityUpgrade.Id].Quantity++;
        }
    }
}
