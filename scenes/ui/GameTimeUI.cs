using Godot;

namespace TwoDSurvivors.GameTime
{
    public partial class GameTimeUI : CanvasLayer
    {
        [Export]
        public GameTimeManager GameTimeManager { get; private set; }
        public Label GameTimeLabel { get; private set; }

        public override void _Ready()
        {
            GameTimeLabel = GetNode<Label>("%GameTimeLabel");
        }

        public override void _Process(double delta)
        {
            if (GameTimeManager is null) return;

            float timeElapsed = GameTimeManager.GetElapsedTime();

            GameTimeLabel.Text = FormatSecondsToString(timeElapsed);
        }

        private string FormatSecondsToString(float seconds)
        {
            int minutes = (int)(seconds / 60);
            int remainingSeconds = (int)(seconds % 60);

            return $"{minutes:00}:{remainingSeconds:00}";
        }
    }
}
