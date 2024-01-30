using Godot;

namespace TwoDSurvivors.Autoload;

public partial class GameEvents : Node
{
    [Signal]
    public delegate void XPVialCollectedEventHandler(float number);

    public void EmitXPVialCollected(float number)
    {
        _ = EmitSignal(SignalName.XPVialCollected, number);
    }
}
