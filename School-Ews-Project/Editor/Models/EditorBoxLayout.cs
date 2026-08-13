using System.Numerics;
using Raylib_cs;

namespace Program;

public enum EditorBoxNames
{
    Header,
    TextPanel,
    Terminal
}

public class EditorBoxLayout
{
    // header (knopen enz)
    // TextPanel (tekst van algo)
    // terminal (notes)

    public Dictionary<EditorBoxNames, float> BoxPercentageHeights { get; } =
    new Dictionary<EditorBoxNames, float>{
        {EditorBoxNames.Header   , 10f},
        {EditorBoxNames.TextPanel, 60f},
        {EditorBoxNames.Terminal , 30f}
    };

    public Dictionary<EditorBoxNames, Vector2> BoxPixelLengths { get; set; } = 
    new Dictionary<EditorBoxNames, Vector2>{
        {EditorBoxNames.Header   , new Vector2(0,0)},
        {EditorBoxNames.TextPanel, new Vector2(0,0)},
        {EditorBoxNames.Terminal , new Vector2(0,0)}
    };

    public Dictionary<EditorBoxNames, Vector2> BoxPixelPositions { get; set; } =
    new Dictionary<EditorBoxNames, Vector2>{
        {EditorBoxNames.Header   , new Vector2(0,0)},
        {EditorBoxNames.TextPanel, new Vector2(0,0)},
        {EditorBoxNames.Terminal , new Vector2(0,0)}
    };

    public Dictionary<EditorBoxNames, Color> BoxLayoutColors { get; set; } =
    new Dictionary<EditorBoxNames, Color>{
        {EditorBoxNames.Header   , new Color(43, 46, 49, 255)},
        {EditorBoxNames.TextPanel, new Color(51, 59, 67, 255)},
        {EditorBoxNames.Terminal , new Color(53, 56, 61, 255)}
    };
}