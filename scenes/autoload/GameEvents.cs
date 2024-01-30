using Godot;

namespace TwoDSurvivors.GameEvents;

public partial class GameEvents : Node
{
    [Signal]
    public delegate void XPVialCollectedEventHandler(float number);

    public void EmitXPVialCollected(float number)
    {
        EmitSignal(SignalName.XPVialCollected, number);
    }
}
