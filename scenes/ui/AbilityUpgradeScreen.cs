using Godot;
using TwoDSurvivors.Resources;

namespace TwoDSurvivors.UI;

public partial class AbilityUpgradeScreen : CanvasLayer
{
    [Signal]
    public delegate void AbilityUpgradeSelectedEventHandler(AbilityUpgrade abilityUpgrade);

    [Export]
    private PackedScene AbilityUpgradeCardScene;
    public HBoxContainer CardContainer { get; private set; }

    public override void _Ready()
    {
        CardContainer = GetNode<HBoxContainer>("MarginContainer/CardContainer");
        GetTree().Paused = true;
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

            cardInstance.Selected += () =>
            {
                // Bind ability upgrade to signal
                HandleAbilityUpgradeSelected(abilityUpgrade);
            };
        }
    }

    private void HandleAbilityUpgradeSelected(AbilityUpgrade abilityUpgrade)
    {
        _ = EmitSignal(SignalName.AbilityUpgradeSelected, abilityUpgrade);
        GetTree().Paused = false;
        QueueFree();
    }
}
