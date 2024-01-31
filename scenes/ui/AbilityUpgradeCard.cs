using Godot;
using TwoDSurvivors.Resources;

namespace TwoDSurvivors.UI;

public partial class AbilityUpgradeCard : PanelContainer
{
    public Label NameLabel { get; private set; }
    public Label DescriptionLabel { get; private set; }

    public override void _Ready()
    {
        NameLabel = GetNode<Label>("VBoxContainer/NameLabel");
        DescriptionLabel = GetNode<Label>("VBoxContainer/DescriptionLabel");
    }

    public override void _Process(double delta) { }

    public void SetAbilityUpgrade(AbilityUpgrade abilityUpgrade)
    {
        // Add upgrade's name and description to labels.
        NameLabel.Text = abilityUpgrade.Name;
        DescriptionLabel.Text = abilityUpgrade.Description;
    }
}
