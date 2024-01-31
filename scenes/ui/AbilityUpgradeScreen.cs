using Godot;
using TwoDSurvivors.Resources;

namespace TwoDSurvivors.UI;

public partial class AbilityUpgradeScreen : CanvasLayer
{
    [Export]
    private PackedScene AbilityUpgradeCardScene;
    public HBoxContainer CardContainer { get; private set; }

    public override void _Ready()
    {
        CardContainer = GetNode<HBoxContainer>("MarginContainer/CardContainer");
    }

    public override void _Process(double delta) { }

    public void SetAbilityUpgrades(Godot.Collections.Array<AbilityUpgrade> abilityUpgrades)
    {
        foreach (AbilityUpgrade abilityUpgrade in abilityUpgrades)
        {
            // Instantiate and add as child of HBox
            AbilityUpgradeCard cardInstance = (AbilityUpgradeCard)
                AbilityUpgradeCardScene.Instantiate();
            CardContainer.AddChild(cardInstance);

            // Set ability upgrade labels in cardInstance.
            cardInstance.SetAbilityUpgrade(abilityUpgrade);
        }
    }
}
