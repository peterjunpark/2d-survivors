using Godot;
using TwoDSurvivors.Managers;

namespace TwoDSurvivors.UI;

public partial class XPBar : CanvasLayer
{
    [Export]
    public XPManager XPManager { get; private set; }
    private ProgressBar XPProgressBar;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        XPManager.XPUpdated += HandleXPUpdate;
        XPProgressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar");
        XPProgressBar.Value = 0;
    }

    private void HandleXPUpdate(float currentXP, float targetXP)
    {
        float xpPercent = currentXP / targetXP;
        XPProgressBar.Value = xpPercent;
    }
}
