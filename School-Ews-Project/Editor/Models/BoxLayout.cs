using System.Numerics;

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

    public Dictionary<EditorBoxNames, float> BoxPercentageWidths { get; } =
    new Dictionary<EditorBoxNames, float>{
        {EditorBoxNames.Header   , 100f},
        {EditorBoxNames.TextPanel, 100f},
        {EditorBoxNames.Terminal , 100f}
    };

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

    public Dictionary<EditorBoxNames, Vector2> BoxpixelPositions { get; set; } =
    new Dictionary<EditorBoxNames, Vector2>{
        {EditorBoxNames.Header   , new Vector2(0,0)},
        {EditorBoxNames.TextPanel, new Vector2(0,0)},
        {EditorBoxNames.Terminal , new Vector2(0,0)}
    };
}