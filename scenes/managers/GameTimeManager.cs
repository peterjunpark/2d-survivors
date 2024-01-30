using Godot;

namespace TwoDSurvivors.Managers;
public partial class GameTimeManager : Node
{
    public Timer GameTimer { get; private set; }

    public override void _Ready()
    {
        GameTimer = GetNode<Timer>("GameTimer");
    }

    public float GetElapsedTime()
    {
        return (float)(GameTimer.WaitTime - GameTimer.TimeLeft);
    }

}
