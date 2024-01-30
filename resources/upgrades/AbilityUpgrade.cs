using Godot;

namespace TwoDSurvivors.Resources;

public partial class AbilityUpgrade : Resource
{
    [Export]
    public string Id { get; private set; }

    [Export]
    public string Name { get; private set; }

    [Export(PropertyHint.MultilineText)]
    public string Description { get; private set; }
}
