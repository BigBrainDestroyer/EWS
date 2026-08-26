using System.Numerics;
using Raylib_cs;

namespace Program;

public enum EditorBoxNames
{
    TextPanel,
    Terminal
}

public class EditorBoxLayoutState
{
    public Dictionary<EditorBoxNames, Vector2> BoxPixelLengths { get; set; } = 
    new Dictionary<EditorBoxNames, Vector2>{
        {EditorBoxNames.TextPanel, new Vector2(0,0)},
        {EditorBoxNames.Terminal , new Vector2(0,0)}
    };

    public Dictionary<EditorBoxNames, Vector2> BoxPixelPositions { get; set; } =
    new Dictionary<EditorBoxNames, Vector2>{
        {EditorBoxNames.TextPanel, new Vector2(0,0)},
        {EditorBoxNames.Terminal , new Vector2(0,0)}
    };

    public Dictionary<EditorBoxNames, Color> BoxLayoutColors { get; } =
    new Dictionary<EditorBoxNames, Color>{
        {EditorBoxNames.TextPanel, new Color(51, 59, 67, 255)},
        {EditorBoxNames.Terminal , new Color(53, 56, 61, 255)}
    };
}